using System;
using System.Collections.Generic;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using Sharpy.Properties;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data by using <see cref="Random"/>.
    ///         To get the same result every time you execute the program use the seed overload constructor.
    ///         If want you to add your own methods you can derive from this class.
    ///     </para>
    ///     <para>
    ///         For examples please visit https://inputfalken.github.io/sharpy-API/
    ///     </para>
    /// </summary>
    public class Provider : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider {
        private readonly DateGenerator _dateGenerator;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IIntegerProvider _integerProvider;

        private readonly Lazy<string[]> _lazyUsernames =
            new Lazy<string[]>(() => Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None));

        private readonly ILongProvider _longProvider;
        private readonly EmailBuilder _mailbuilder;
        private readonly INameProvider _nameProvider;
        private readonly NumberGenerator _numberGenerator;
        private readonly Random _random;
        private readonly SecurityNumberGen _securityNumberGen;


        private readonly bool _uniqueNumbers;
        private Tuple<int, int> _numberByLengthState = new Tuple<int, int>(0, 0);


        /// <summary>
        ///     <para>
        ///         Returns a provider with your configurement
        ///     </para>
        /// </summary>
        /// <param name="configurement"></param>
        public Provider(Configurement configurement) {
            _random = configurement.Random;
            _doubleProvider = configurement.DoubleProvider;
            _integerProvider = configurement.IntegerProvider;
            _longProvider = configurement.LongProvider;
            _nameProvider = configurement.NameProvider;
            _dateGenerator = configurement.DateGenerator;
            _mailbuilder = configurement.EmailBuilder;
            _securityNumberGen = configurement.SecurityNumberGen;
            _numberGenerator = configurement.NumberGenerator;
            _uniqueNumbers = configurement.UniqueNumbers;
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Provider"/> which will Randomize the same result by the seed.
        ///     </para>
        /// </summary>
        /// <param name="seed"></param>
        public Provider(int seed) : this(new Configurement(seed)) { }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Provider"/> which will randomize with the random supplied.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        public Provider(Random random) : this(new Configurement(random)) { }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Provider"/> which will randomize new results every time program is executed.
        ///     </para>
        /// </summary>
        public Provider() : this(new Configurement()) { }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="double"/>.
        ///     </para>
        /// </summary>
        public double Double() {
            return _doubleProvider.Double();
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="double"/> within argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="max"></param>
        public double Double(double max) {
            return _doubleProvider.Double(max);
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="double"/> within argument <paramref name="min"/> and argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public double Double(double min, double max) {
            return _doubleProvider.Double(min, max);
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="int"/>.
        ///     </para>
        /// </summary>
        /// <param name="max"></param>
        public int Integer(int max) {
            return _integerProvider.Integer(max);
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="int"/> within argument <paramref name="min"/> and argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public int Integer(int min, int max) {
            return _integerProvider.Integer(min, max);
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="int"/>.
        ///     </para>
        /// </summary>
        public int Integer() {
            return _integerProvider.Integer();
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="long"/> within argument <paramref name="min"/> and <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public long Long(long min, long max) {
            return _longProvider.Long(min, max);
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="long"/> within argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="max"></param>
        public long Long(long max) {
            return _longProvider.Long(max);
        }

        /// <summary>
        ///     <para>
        ///         Generates a <see cref="long"/>.
        ///     </para>
        /// </summary>
        public long Long() {
            return _longProvider.Long();
        }


        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string"/> representing a first name.
        ///     </para>
        /// </summary>
        public string FirstName() {
            return _nameProvider.FirstName();
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string"/> representing a first name based on <see cref="Gender"/>.
        ///     </para>
        /// </summary>
        /// <param name="gender"></param>
        public string FirstName(Gender gender) {
            return _nameProvider.FirstName(gender);
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string"/> representing a last name.
        ///     </para>
        /// </summary>
        public string LastName() {
            return _nameProvider.LastName();
        }

        /// <summary>
        ///     <para>
        ///         Randomizes one of the arguments.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public T Params<T>(params T[] items) {
            return items.RandomItem(_random);
        }

        /// <summary>
        ///     <para>
        ///         Randomizes one of the elements.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public T CustomCollection<T>(IReadOnlyList<T> items) {
            return items.RandomItem(_random);
        }

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="bool"/>.
        ///     </para>
        /// </summary>
        public bool Bool() {
            return _random.Next(2) != 0;
        }

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="LocalDate"/> based on argument <paramref name="age"/>.
        ///     </para>
        /// </summary>
        /// <param name="age"></param>
        public LocalDate DateByAge(int age) {
            return _dateGenerator.RandomDateByAge(age);
        }

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="LocalDate"/> based on argument <paramref name="year"/>.
        ///     </para>
        /// </summary>
        /// <param name="year"></param>
        public LocalDate DateByYear(int year) {
            return _dateGenerator.RandomDateByYear(year);
        }

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="string"/> representing a unique social-security number.
        ///     </para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated"></param>
        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var result = _securityNumberGen.SecurityNumber(_random.Next(10000),
                FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maxium possible combinations for a controlnumber");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(result, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string"/> representing a mailaddress.
        ///     </para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        public string MailAddress(string name, string secondName = null) {
            return _mailbuilder.Mail(name, secondName);
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="int"/> with the length of the number given to argument <paramref name="length"/>.
        ///     </para>
        /// </summary>
        /// <param name="length"></param>
        public string NumberByLength(int length) {
            //If phonestate has changed
            if (_numberByLengthState.Item1 != length)
                _numberByLengthState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var res = _numberGenerator.RandomNumber(0, _numberByLengthState.Item2, _uniqueNumbers);
            if (res == -1) throw new Exception($"You reached maxium amount of combinations for the {nameof(length)} used");

            var number = res.ToString();
            return number.Length != length
                ? Prefix(number, length - number.Length)
                : number;
        }

        /// <summary>
        ///     <para>
        ///         Returns a random <see cref="string"/> representing a username.
        ///     </para>
        /// </summary>
        public string UserName() {
            return _lazyUsernames.Value.RandomItem(_random);
        }

        private static string Prefix<T>(T item, int ammount) {
            return new string('0', ammount).Append(item);
        }

        private static string FormatDigit(int i) {
            return i < 10 ? Prefix(i, 1) : i.ToString();
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}"/> with <see cref="Provider"/> as its generic type.
        ///     </para>
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>
        ///     <para>
        ///         A <see cref="IGenerator{T}"/>.
        ///     </para>
        /// </returns>
        public static IGenerator<TProvider> AsGenerator<TProvider>(TProvider provider) where TProvider : Provider {
            return Generator.Create(provider);
        }
    }
}