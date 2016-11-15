using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    ///     <para>My implementation of IGenerator</para>
    ///     <para>Contains properties which you can optionally set to change the behavior of the Generator.</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    public sealed class Generator : IGenerator<StringType> {
        private const string NoSet = "None Set";

        private readonly HashSet<Enum> _origins = new HashSet<Enum>();
        private Randomizer<Name> _names;
        private Tuple<int, int> _phoneState;

        private string _seed;

        private Randomizer<string> _userNames;

        /// <summary>
        ///     <para>Instantiates a new Generator</para>
        /// </summary>
        public Generator() {
            DateGenerator = new DateGenerator(Random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random);
            NumberGen = new NumberGenerator(Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(Random);
            PhoneNumberGenerator = new NumberGenerator(Random);
        }


        private NumberGenerator PhoneNumberGenerator { get; }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }

        private Lazy<Randomizer<Name>> LazyNames { get; } =
            new Lazy<Randomizer<Name>>(() => new Randomizer<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Randomizer<Name> Names {
            get { return _names ?? LazyNames.Value; }
            private set { _names = value; }
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

        private Dictionary<StringType, Randomizer<string>> Dictionary { get; } =
            new Dictionary<StringType, Randomizer<string>>();


        /// <summary>
        ///     <para>Sets the predicate which will be executed on each Firstname and Lastname.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument.</para>
        /// </summary>
        public Func<string, bool> NamePredicate {
            set { Names = new Randomizer<Name>(Names.Where(name => value(name.Data))); }
        }


        /// <summary>
        ///     <para>Sets Countries which Firstname and Lastname are from.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument.</para>
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Country> Countries {
            set {
                foreach (var country in value) _origins.Add(country);
                Names = new Randomizer<Name>(Names.Where(name => value.Contains(name.Country)));
            }
        }

        /// <summary>
        ///     <para>Sets Regions which Firstname and Lastname are from.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument</para>
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Region> Regions {
            set {
                foreach (var region in value) _origins.Add(region);
                Names = new Randomizer<Name>(Names.Where(name => value.Contains(name.Region)));
            }
        }

        /// <summary>
        ///     <para>Sets the mailproviders which will be used for generating MailAddresses.</para>
        ///     <para>This affects IGenerator's MailAddress method.</para>
        /// </summary>
        //public void MailProviders(params string[] providers) => Mailgen.EmailDomains = providers;
        public IReadOnlyList<string> MailProviders {
            set { Mailgen.EmailDomains = value; }
        }

        /// <summary>
        ///     <para>Sets if mailaddresses are gonna be unique.</para>
        ///     <para>This affects IGenerator's MailAddress method.</para>
        /// </summary>
        public bool UniqueMailAddresses {
            set { Mailgen.Unique = value; }
        }


        /// <summary>
        ///     <para>Sets the predicate which will be executed on each UserName.</para>
        ///     <para>This affects IGenerator's String method when you use UserName as argument.</para>
        /// </summary>
        public Func<string, bool> UserNamePredicate {
            set { UserNames = new Randomizer<string>(UserNames.Where(value)); }
        }


        /// <summary>
        ///     <para>Sets the seed for Generator.</para>
        ///     <para>This affects every method in IGenerator to generate same results everytime the program is executed.</para>
        /// </summary>
        public int Seed {
            set {
                _seed = value.ToString();
                Random = new Random(value);
            }
        }


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

        string IGenerator<StringType>.MailAddress(string name, string secondName)
            => Mailgen.Mail(name, secondName);

        // The combinations possible is 10^length
        string IGenerator<StringType>.PhoneNumber(int length, string prefix) {
            //If phonestate has changed
            if ((_phoneState == null) || (_phoneState.Item1 != length))
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

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();


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

        private IEnumerable<string> StringType(StringType stringType) {
            switch (stringType) {
                case Enums.StringType.FemaleFirstName:
                    return Names.Where(name => name.Type == 1).Select(name => name.Data);
                case Enums.StringType.MaleFirstName:
                    return Names.Where(name => name.Type == 2).Select(name => name.Data);
                case Enums.StringType.LastName:
                    return Names.Where(name => name.Type == 3).Select(name => name.Data);
                case Enums.StringType.FirstName:
                    return Names.Where(name => (name.Type == 1) | (name.Type == 2)).Select(name => name.Data);
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