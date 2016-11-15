using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.DataObjects;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.Implementation.Generators;
using Sharpy.Properties;

namespace Sharpy {
    /// <summary>
    ///     <para>Static generator using my implementation of IGenerator</para>
    ///     <para>Can also create instances of the generator</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    /// <returns></returns>
    public sealed class Generator : IGenerator<StringType> {
        private Tuple<int, int> _phoneState;

        static Generator() {
            StaticGen = Create();
        }


        private static Generator StaticGen { get; }


        T IGenerator<StringType>.Params<T>(params T[] items) => items[Random.Next(items.Length)];

        T IGenerator<StringType>.CustomCollection<T>(IList<T> items) => items[Random.Next(items.Count)];

        string IGenerator<StringType>.String(StringType type) {
            if (!Dictionary.ContainsKey(type))
                Dictionary.Add(type, new Randomizer<string>(StringType(type)));
            return Dictionary[type].RandomItem(Random);
        }

        bool IGenerator<StringType>.Bool() => Random.Next(2) != 0;

        int IGenerator<StringType>.Integer(int max) {
            IGenerator<StringType> foo = this;
            return foo.Integer(0, max);
        }

        int IGenerator<StringType>.Integer(int min, int max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return Random.Next(min, max);
        }

        int IGenerator<StringType>.Integer() => Random.Next(int.MinValue, int.MaxValue);

        LocalDate IGenerator<StringType>.DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        LocalDate IGenerator<StringType>.DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        string IGenerator<StringType>.SocialSecurityNumber(LocalDate date, bool formated) {
            var securityNumber =
                SocialSecurityNumberGenerator
                    .SecurityNumber(Random.Next(10000),
                        FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)))
                    .ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(securityNumber, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();

        string IGenerator<StringType>.MailAddress(string name, string secondName)
            => Mailgen.Mail(name, secondName);

        // The combinations possible is 10^length
        string IGenerator<StringType>.PhoneNumber(int length, string prefix) {
            //If phonestate has changed
            if (_phoneState == null || _phoneState.Item1 != length)
                _phoneState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var randomNumber = PhoneNumberGenerator.RandomNumber(0, _phoneState.Item2, true).ToString();
            return randomNumber.Length != length
                ? prefix + Prefix(randomNumber, length - randomNumber.Length)
                : prefix + randomNumber;
        }

        long IGenerator<StringType>.Long(long min, long max) => Random.NextLong(min, max);

        long IGenerator<StringType>.Long(long max) => Random.NextLong(max);

        long IGenerator<StringType>.Long() => Random.NextLong();

        double IGenerator<StringType>.Double() => Random.NextDouble();

        double IGenerator<StringType>.Double(double max) => Random.NextDouble(max);

        double IGenerator<StringType>.Double(double min, double max) => Random.NextDouble(min, max);

        /// <summary>
        ///     <para>Creates a new instance of Generator.</para>
        /// </summary>
        /// <returns></returns>
        public static Generator Create() => new Generator();

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IGenerator<StringType>, T> func, int count = 10)
            => StaticGen.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        ///     <para>Includes an integer counting iterations.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IGenerator<StringType>, int, T> func, int count = 10)
            => StaticGen.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a &lt;T&gt;.</para>
        /// </summary>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IGenerator<StringType>, T> func)
            => StaticGen.Generate(func);

        /// <inheritdoc />
        public override string ToString() {
            var origins = string.Empty;
            foreach (var origin in _origins)
                if (origin.Equals(_origins.Last())) origins += origin;
                else origins += $"{origin}, ";
            return
                $"Seed: {_seed ?? NoSet}. Using default for System.Random\n" +
                $"Mail: {Mailgen}\n" +
                $"Name: Origins: {(string.IsNullOrEmpty(origins) ? NoSet : origins)}";
        }

        private const string NoSet = "None Set";
        private NumberGenerator PhoneNumberGenerator { get; }

        private readonly HashSet<Enum> _origins = new HashSet<Enum>();
        private Randomizer<Name> _names;

        private string _seed;

        private Randomizer<string> _userNames;

        public Generator() {
            DateGenerator = new DateGenerator(Random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random);
            NumberGen = new NumberGenerator(Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(Random);
            PhoneNumberGenerator = new NumberGenerator(Random);
        }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }

        private Lazy<Randomizer<Name>> LazyNames { get; } =
            new Lazy<Randomizer<Name>>(() => new Randomizer<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Randomizer<Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }


        private Random Random { get; set; } = new Random();
        private DateGenerator DateGenerator { get; }


        private NumberGenerator NumberGen { get; }


        private MailGenerator Mailgen { get; }

        private Lazy<Randomizer<string>> LazyUsernames { get; } =
            new Lazy<Randomizer<string>>(
                () => new Randomizer<string>(Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)));

        private Randomizer<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }

        internal Dictionary<StringType, Randomizer<string>> Dictionary { get; } =
            new Dictionary<StringType, Randomizer<string>>();


        /// <summary>
        /// Executes the predicate on each firstname/lastname.
        /// </summary>
        public Func<string, bool> NamePredicate {
            set { Names = new Randomizer<Name>(Names.Where(name => value(name.Data))); }
        }


        /// <summary>
        ///     Sets Countries which Firstname/lastname are from.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Country> NameCountries {
            set {
                foreach (var country in value) _origins.Add(country);
                Names = new Randomizer<Name>(Names.Where(name => value.Contains(name.Country)));
            }
        }

        /// <summary>
        ///     Sets Regions which Firstname/lastname are from.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Region> NameRegion {
            set {
                foreach (var region in value) _origins.Add(region);
                Names = new Randomizer<Name>(Names.Where(name => value.Contains(name.Region)));
            }
        }

        /// <summary>
        /// <para>Sets the mailproviders which will be used for mailgenerator.</para>
        /// </summary>
        //public void MailProviders(params string[] providers) => Mailgen.EmailDomains = providers;
        public IReadOnlyList<string> MailProviders {
            set { Mailgen.EmailDomains = value; }
        }

        /// <summary>
        /// <para>Sets if mailaddresses are gonna be unique.</para>
        /// </summary>
        public bool UniqueMailAddresses {
            set { Mailgen.Unique = value; }
        }


        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <returns></returns>
        public Func<string, bool> UserNamePredicate {
            set { UserNames = new Randomizer<string>(UserNames.Where(value)); }
        }


        /// <summary>
        ///     Sets the seed for Generator.
        /// </summary>
        public int Seed {
            set {
                _seed = value.ToString();
                Random = new Random(value);
            }
        }

        internal IEnumerable<string> StringType(StringType stringType) {
            switch (stringType) {
                case Enums.StringType.FemaleFirstName:
                    return Names.Where(name => name.Type == 1).Select(name => name.Data);
                case Enums.StringType.MaleFirstName:
                    return Names.Where(name => name.Type == 2).Select(name => name.Data);
                case Enums.StringType.LastName:
                    return Names.Where(name => name.Type == 3).Select(name => name.Data);
                case Enums.StringType.FirstName:
                    return Names.Where(name => name.Type == 1 | name.Type == 2).Select(name => name.Data);
                case Enums.StringType.UserName:
                    return UserNames;
                case Enums.StringType.AnyName:
                    return Names.Select(name => name.Data).Concat(UserNames);
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringType), stringType, null);
            }
        }
    }
}