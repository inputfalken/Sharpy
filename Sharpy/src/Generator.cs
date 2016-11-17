using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation.DataObjects;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.Implementation.Generators;
using static Sharpy.Enums.StringType;
using static Sharpy.Properties.Resources;

namespace Sharpy {
    /// <summary>
    ///     <para> My implementation of IGenerator</para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         By default multiple generator instances will generate the same results each execution.
    ///     </para>
    ///     <para> Contains properties which you can optionally set to change the behavior of the Generator.</para>
    ///     <para> For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </remarks>
    public sealed class Generator : IGenerator<StringType> {
        /// <summary>
        ///     <para>This captures the current Ticks once each time the program is executed.</para>
        ///     <para>Multiple generators will have the same seed.</para>
        /// </summary>
        private static readonly int DefaultSeed = (int) SystemClock.Instance.Now.Ticks & 0x0000FFFF;

        private IEnumerable<Name> _names;
        private Tuple<int, int> _phoneState = new Tuple<int, int>(0, 0);

        private int _seed = DefaultSeed;

        private IEnumerable<string> _userNames;

        /// <summary>
        ///     <para>Instantiates a new Generator</para>
        /// </summary>
        public Generator() {
            Random = new Random(Seed);
            DateGenerator = new DateGenerator(Random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(Random);
            PhoneNumberGenerator = new NumberGenerator(Random);
        }


        private NumberGenerator PhoneNumberGenerator { get; }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }

        private Lazy<IEnumerable<Name>> LazyNames { get; } =
            new Lazy<IEnumerable<Name>>(() => JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(NamesByOrigin)));

        private IEnumerable<Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }


        private Random Random { get; set; }
        private DateGenerator DateGenerator { get; }


        private MailGenerator Mailgen { get; }

        private Lazy<IEnumerable<string>> LazyUsernames { get; } =
            new Lazy<IEnumerable<string>>(() => usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None));

        private IEnumerable<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }

        private Dictionary<StringType, IReadOnlyList<string>> Dictionary { get; } =
            new Dictionary<StringType, IReadOnlyList<string>>();


        /// <summary>
        ///     <para>Sets the predicate which will be executed on each Firstname and Lastname.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument.</para>
        /// </summary>
        public Func<string, bool> NamePredicate {
            set { Names = Names.Where(name => value(name.Data)); }
        }


        /// <summary>
        ///     <para>Gets and Sets Countries which Firstname and Lastname are from.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument.</para>
        ///     <para>Set to all countries by default.</para>
        /// </summary>
        public IEnumerable<Country> Countries { get; set; }

        /// <summary>
        ///     <para>Gets and Sets Regions which Firstname and Lastname are from.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument</para>
        ///     <para>Set to all regions by default.</para>
        /// </summary>
        public IEnumerable<Region> Regions { get; set; }

        /// <summary>
        ///     <para>Gets and Sets the mailproviders which will be used for generating MailAddresses.</para>
        ///     <para>This affects IGenerator's MailAddress method.</para>
        ///     <para>Set to gmail.com, hotmail.com and yahoo.com by default.</para>
        /// </summary>
        public IEnumerable<string> MailProviders {
            get { return Mailgen.EmailDomains; }
            set { Mailgen.EmailDomains = value.ToArray(); }
        }

        /// <summary>
        ///     <para>Gets and Sets if mailaddresses are gonna be unique.</para>
        ///     <para>This affects IGenerator's MailAddress method.</para>
        ///     <para>Set to true by Default</para>
        /// </summary>
        public bool UniqueMailAddresses {
            get { return Mailgen.Unique; }
            set { Mailgen.Unique = value; }
        }

        /// <summary>
        ///     <para>Gets and Sets if phone numbers are gonna be unique.</para>
        ///     <para>This affects IGenerator's PhoneAddress method.</para>
        ///     <para>Set to true by Default</para>
        /// </summary>
        public bool UniquePhoneNumbers { get; set; } = true;

        /// <summary>
        ///     <para>Sets the predicate which will be executed on each UserName.</para>
        ///     <para>This affects IGenerator's String method when you use UserName as argument.</para>
        /// </summary>
        public Func<string, bool> UserNamePredicate {
            set { UserNames = UserNames.Where(value); }
        }


        /// <summary>
        ///     <para>Gets And Sets the seed for Generator.</para>
        ///     <para>This affects every method in IGenerator to generate same results everytime the program is executed.</para>
        ///     <para>Set to the Tick when program executes by default.</para>
        /// </summary>
        public int Seed {
            internal get { return _seed; }
            set {
                _seed = value;
                Random = new Random(value);
            }
        }


        T IGenerator<StringType>.Params<T>(params T[] items) => items.RandomItem(Random);

        T IGenerator<StringType>.CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(Random);

        string IGenerator<StringType>.String(StringType type) {
            if (Dictionary.ContainsKey(type)) return Dictionary[type].RandomItem(Random);
            var strings = StringType(type).ToArray();
            if (strings.Any()) Dictionary.Add(type, strings);
            else throw new Exception("Can't obtain strings with this configuration");
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
            var result = SocialSecurityNumberGenerator.SecurityNumber(Random.Next(10000),
                FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maxium possible combinations for a controlnumber");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(result, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        string IGenerator<StringType>.MailAddress(string name, string secondName)
            => Mailgen.Mail(name, secondName);

        // The combinations possible is 10^length
        string IGenerator<StringType>.PhoneNumber(int length, string prefix) {
            //If phonestate has changed
            if (_phoneState.Item1 != length)
                _phoneState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var res = PhoneNumberGenerator.RandomNumber(0, _phoneState.Item2, UniquePhoneNumbers);
            if (res == -1) throw new Exception("You reached maxium Ammount of combinations for the Length used");

            var phoneNumber = res.ToString();
            return phoneNumber.Length != length
                ? prefix + Prefix(phoneNumber, length - phoneNumber.Length)
                : prefix + phoneNumber;
        }

        long IGenerator<StringType>.Long(long min, long max) => Random.NextLong(min, max);

        long IGenerator<StringType>.Long(long max) => Random.NextLong(max);

        long IGenerator<StringType>.Long() => Random.NextLong();

        double IGenerator<StringType>.Double() => Random.NextDouble();

        double IGenerator<StringType>.Double(double max) => Random.NextDouble(max);

        double IGenerator<StringType>.Double(double min, double max) => Random.NextDouble(min, max);

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();

        private IEnumerable<string> StringType(StringType stringType) {
            switch (stringType) {
                case FemaleFirstName:
                    return Origin(Names.Where(name => name.Type == 1)).Select(name => name.Data);
                case MaleFirstName:
                    return Origin(Names.Where(name => name.Type == 2)).Select(name => name.Data);
                case LastName:
                    return Origin(Names.Where(name => name.Type == 3)).Select(name => name.Data);
                case FirstName:
                    return Origin(Names.Where(name => (name.Type == 1) | (name.Type == 2))).Select(name => name.Data);
                case UserName:
                    return UserNames;
                case AnyName:
                    return Names.Select(name => name.Data).Concat(UserNames);
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringType), stringType, null);
            }
        }

        private IEnumerable<Name> Origin(IEnumerable<Name> names) {
            var region = Regions != null && Regions.Any();
            var country = Countries != null && Countries.Any();
            if (region && country)
                return names.Where(name => Regions.Contains(name.Region))
                    .Where(name => Countries.Contains(name.Country));
            if (region) return names.Where(name => Regions.Contains(name.Region));
            return country ? names.Where(name => Countries.Contains(name.Country)) : names;
        }
    }
}