using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.Implementation.Generators;

namespace Sharpy {
    /// <summary>
    ///     <para>My implementation of IGenerator</para>
    /// </summary>
    /// <remarks>
    ///     <para>Contains properties which you can optionally set to change the behavior of the Generator.</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </remarks>
    public sealed class Generator : IGenerator {
        private Tuple<int, int> _phoneState = new Tuple<int, int>(0, 0);

        /// <summary>
        ///     <para>Instantiates a new generator which will generate same results every execution based on seed</para>
        /// </summary>
        public Generator(int? seed = null) {
            Seed = seed ?? SeedByTick;
            Random = new Random(Seed);
            DoubleProvider = new DoubleRandomizer(Random);
            IntegerProvider = new IntRandomizer(Random);
            NameProvider = new NameFetcher(Random);
            LongProvider = new LongRandomizer(Random);
            DateGenerator = new DateGenerator(Random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(Random);
            PhoneNumberGenerator = new NumberGenerator(Random);
        }

        public INameProvider NameProvider { get; set; }
        public IDoubleProvider DoubleProvider { get; set; }
        public IIntegerProvider IntegerProvider { get; set; }
        public ILongProvider LongProvider { get; set; }

        /// <summary>
        ///     <para>This captures the current Ticks once each time invoked.</para>
        /// </summary>
        private static int SeedByTick => (int) SystemClock.Instance.Now.Ticks & 0x0000FFFF;


        private NumberGenerator PhoneNumberGenerator { get; }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }


        private Random Random { get; }
        private DateGenerator DateGenerator { get; }


        private MailGenerator Mailgen { get; }


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
        ///     <para>Gets the seed for Generator.</para>
        /// </summary>
        public int Seed { get; }


        T IGenerator.Params<T>(params T[] items) => items.RandomItem(Random);

        T IGenerator.CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(Random);


        bool IGenerator.Bool() => Random.Next(2) != 0;


        LocalDate IGenerator.DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        LocalDate IGenerator.DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        string IGenerator.SocialSecurityNumber(LocalDate date, bool formated) {
            var result = SocialSecurityNumberGenerator.SecurityNumber(Random.Next(10000),
                FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maxium possible combinations for a controlnumber");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(result, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        string IGenerator.MailAddress(string name, string secondName)
            => Mailgen.Mail(name, secondName);

        // The combinations possible is 10^length
        string IGenerator.PhoneNumber(int length, string prefix) {
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


        int IIntegerProvider.Integer(int max) => IntegerProvider.Integer(max);

        int IIntegerProvider.Integer(int min, int max) => IntegerProvider.Integer(min, max);

        int IIntegerProvider.Integer() => IntegerProvider.Integer();

        double IDoubleProvider.Double() => DoubleProvider.Double();

        double IDoubleProvider.Double(double max) => DoubleProvider.Double(max);

        double IDoubleProvider.Double(double min, double max) => DoubleProvider.Double(min, max);

        string INameProvider.Name(NameType arg) => NameProvider.Name(arg);

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();

        long ILongProvider.Long(long min, long max) => LongProvider.Long(min, max);

        long ILongProvider.Long(long max) => LongProvider.Long(max);

        long ILongProvider.Long() => LongProvider.Long();
    }
}