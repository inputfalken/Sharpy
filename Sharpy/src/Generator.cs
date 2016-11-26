using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy {
    /// <summary>
    ///     <para>My implementation of IGenerator</para>
    /// </summary>
    /// <remarks>
    ///     <para>Contains properties which you can optionally set to change the behavior of the Generator.</para>
    ///     <para>If you want to generate data just call Generate/GenerateMany.</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </remarks>
    public sealed class Generator : GeneratorBase {
        private readonly Random _random;
        private Tuple<int, int> _phoneState = new Tuple<int, int>(0, 0);

        /// <summary>
        ///     <para>Instantiates a new generator which will generate same results every execution based on seed</para>
        ///     <para>If you don't set the seed, it will be set by the current Tick</para>
        /// </summary>
        public Generator(Random random = null)
            : base(
                new DoubleRandomizer(random), new IntRandomizer(random), new UserNameRandomizer(random),
                new NameByOrigin(random), new LongRandomizer(random)) {
            _random = random;
            DateGenerator = new DateGenerator(_random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, _random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(_random);
            PhoneNumberGenerator = new NumberGenerator(_random);
        }

        private NumberGenerator PhoneNumberGenerator { get; }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }


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
        ///     <para>Gets and Sets if IGenerator's NumberByLength returns unique numbers.</para>
        ///     <para>Set to true by Default</para>
        ///     <para>
        ///         NOTE:
        ///         If this is true the following will happen.
        ///         IGenerator's NumberByLength method will throw an exception if called more than Length^10
        ///     </para>
        /// </summary>
        public bool UniqueNumbers { get; set; } = true;

        /// <summary>
        ///     <para>Gets the seed for Generator.</para>
        /// </summary>
        public int Seed { get; }

        public T Params<T>(params T[] items) => items.RandomItem(_random);

        public T CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(_random);

        public bool Bool() => _random.Next(2) != 0;

        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var result = SocialSecurityNumberGenerator.SecurityNumber(_random.Next(10000),
                FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maxium possible combinations for a controlnumber");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(result, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        public string MailAddress(string name, string secondName = null)
            => Mailgen.Mail(name, secondName);

        // The combinations possible is 10^length
        public string NumberByLength(int length) {
            //If phonestate has changed
            if (_phoneState.Item1 != length)
                _phoneState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var res = PhoneNumberGenerator.RandomNumber(0, _phoneState.Item2, UniqueNumbers);
            if (res == -1) throw new Exception("You reached maxium Ammount of combinations for the Length used");

            var phoneNumber = res.ToString();
            return phoneNumber.Length != length
                ? Prefix(phoneNumber, length - phoneNumber.Length)
                : phoneNumber;
        }

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();
    }
}