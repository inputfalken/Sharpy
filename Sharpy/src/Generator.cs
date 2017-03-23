using System;
using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using Sharpy.Properties;

//TODO create static Generation method which uses a new generator every time used.

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         A custom delegate with generation purpose.
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate T Generator<out T>();

    /// <summary>
    ///     <para>
    ///         Contains various methods for generating data.
    ///         To get the same result every time you execute the program use the seed overload constructor.
    ///         If want you to add your own methods you can derive from this class.
    ///     </para>
    ///     <para>
    ///         To generate any type call Generate/GenerateSequence.
    ///     </para>
    ///     <para>
    ///         For examples please visit https://inputfalken.github.io/sharpy-API/
    ///     </para>
    /// </summary>
    public class Generator : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider {
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
        private readonly SecurityNumberGen _socialSecurityNumberGenerator;


        private readonly bool _uniqueNumbers;
        private Tuple<int, int> _phoneState = new Tuple<int, int>(0, 0);


        /// <summary>
        ///     <para>
        ///         Returns a generator with your configurement
        ///     </para>
        /// </summary>
        /// <param name="configurement"></param>
        public Generator(Configurement configurement) {
            _random = configurement.Random;
            _doubleProvider = configurement.DoubleProvider;
            _integerProvider = configurement.IntegerProvider;
            _longProvider = configurement.LongProvider;
            _nameProvider = configurement.NameProvider;
            _dateGenerator = configurement.DateGenerator;
            _mailbuilder = configurement.EmailBuilder;
            _socialSecurityNumberGenerator = configurement.SecurityNumberGen;
            _numberGenerator = configurement.NumberGenerator;
            _uniqueNumbers = configurement.UniqueNumbers;
        }

        /// <summary>
        ///     <para>
        ///         Returns a generator which will Randomize the same result by the seed.
        ///     </para>
        /// </summary>
        /// <param name="seed"></param>
        public Generator(int seed) : this(new Configurement(seed)) {}

        /// <summary>
        ///     <para>
        ///         Returns a generator which will randomize with the random supplied.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        public Generator(Random random) : this(new Configurement(random)) {}

        /// <summary>
        ///     <para>
        ///         Returns a generator which will randomize new results every time program is executed.
        ///     </para>
        /// </summary>
        public Generator() : this(new Configurement()) {}

        /// <summary>
        ///     <para>
        ///         Generates a double.
        ///     </para>
        /// </summary>
        public double Double() => _doubleProvider.Double();

        /// <summary>
        ///     <para>
        ///         Generates a double within max value.
        ///     </para>
        /// </summary>
        /// <param name="max"></param>
        public double Double(double max) => _doubleProvider.Double(max);

        /// <summary>
        ///     <para>
        ///         Generates a within min and max.
        ///     </para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <summary>
        ///     <para>
        ///         Generates a integer.
        ///     </para>
        /// </summary>
        /// <param name="max"></param>
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <summary>
        ///     <para>
        ///         Generates a integer within min and max.
        ///     </para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <summary>
        ///     <para>
        ///         Generates a integer.
        ///     </para>
        /// </summary>
        public int Integer() => _integerProvider.Integer();

        /// <summary>
        ///     <para>
        ///         Generates a long within min and max.
        ///     </para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <summary>
        ///     <para>
        ///         Generates a long within max.
        ///     </para>
        /// </summary>
        /// <param name="max"></param>
        public long Long(long max) => _longProvider.Long(max);

        /// <summary>
        ///     <para>
        ///         Generates a long.
        ///     </para>
        /// </summary>
        public long Long() => _longProvider.Long();


        /// <summary>
        ///     <para>
        ///         Returns a string representing a first name.
        ///     </para>
        /// </summary>
        public string FirstName() => _nameProvider.FirstName();

        /// <summary>
        ///     <para>
        ///         Returns a string representing a first name based on Gender.
        ///     </para>
        /// </summary>
        /// <param name="gender"></param>
        public string FirstName(Gender gender) => _nameProvider.FirstName(gender);

        /// <summary>
        ///     <para>
        ///         Returns a string representing a last name.
        ///     </para>
        /// </summary>
        public string LastName() => _nameProvider.LastName();

        /// <summary>
        ///     <para>
        ///         Randomizes one of the arguments.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public T Params<T>(params T[] items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>
        ///         Randomizes one of the elements in the IReadOnlyList.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public T CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>
        ///         Randomizes a bool.
        ///     </para>
        /// </summary>
        public bool Bool() => _random.Next(2) != 0;

        /// <summary>
        ///     <para>
        ///         Randomizes a date based on age.
        ///     </para>
        /// </summary>
        /// <param name="age"></param>
        public LocalDate DateByAge(int age) => _dateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     <para>
        ///         Randomizes a date based on year.
        ///     </para>
        /// </summary>
        /// <param name="year"></param>
        public LocalDate DateByYear(int year) => _dateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     <para>
        ///         Randomizes a unique SocialSecurity Number.
        ///     </para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated"></param>
        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var result = _socialSecurityNumberGenerator.SecurityNumber(_random.Next(10000),
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
        ///         Returns a string representing a mailaddress.
        ///     </para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        public string MailAddress(string name, string secondName = null)
            => _mailbuilder.Mail(name, secondName);

        /// <summary>
        ///     <para>
        ///         Returns a number with the length of the argument.
        ///     </para>
        /// </summary>
        /// <param name="length"></param>
        public string NumberByLength(int length) {
            //If phonestate has changed
            if (_phoneState.Item1 != length)
                _phoneState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var res = _numberGenerator.RandomNumber(0, _phoneState.Item2, _uniqueNumbers);
            if (res == -1) throw new Exception("You reached maxium Ammount of combinations for the Length used");

            var phoneNumber = res.ToString();
            return phoneNumber.Length != length
                ? Prefix(phoneNumber, length - phoneNumber.Length)
                : phoneNumber;
        }

        /// <summary>
        ///     <para>
        ///         Returns a random username from a huge collection.
        ///     </para>
        /// </summary>
        public string UserName() => _lazyUsernames.Value.RandomItem(_random);

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();


        /// <summary>
        ///     <para>
        ///         Creates a new Generator and invokes Generate passing the function.
        ///     </para>
        ///     <para>&#160;</para>
        ///     <remarks>
        ///         This method should not be called more than once.
        ///         Since every invokation will create a new Generator instance.
        ///         Consider using your own Generator instance and call respective method.
        ///     </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static T Element<T>(Func<Generator, T> fn) => new Generator().Generate(fn);

        /// <summary>
        ///     <para>
        ///         Creates a new Generator using the Configurement and invokes Generate passing the function.
        ///     </para>
        ///     <para>&#160;</para>
        ///     <remarks>
        ///         This method should not be called more than once.
        ///         Since every invokation will create a new Generator instance.
        ///         Consider using your own Generator instance and call respective method.
        ///     </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static T Element<T>(Func<Generator, T> fn, Configurement config)
            => new Generator(config).Generate(fn);

        /// <summary>
        ///     <para>
        ///         Creates a new Generator and invokes GenerateSequence passing the function and count.
        ///     </para>
        ///     <para>&#160;</para>
        ///     <remarks>
        ///         This method should not be called more than once.
        ///         Since every invokation will create a new Generator instance.
        ///         Consider using your own Generator instance and call respective method.
        ///     </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> Sequence<T>(Func<Generator, T> fn, int count)
            => new Generator().GenerateSequence(fn, count);

        /// <summary>
        ///     <para>
        ///         Creates a new Generator using the Configurement and invokes GenerateSequence passing the function and count.
        ///     </para>
        ///     <para>&#160;</para>
        ///     <remarks>
        ///         This method should not be called more than once.
        ///         Since every invokation will create a new Generator instance.
        ///         Consider using your own Generator instance and call respective method.
        ///     </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <param name="count"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IEnumerable<T> Sequence<T>(Func<Generator, T> fn, int count, Configurement config)
            => new Generator(config).GenerateSequence(fn, count);

        /// <summary>
        ///     <para>
        ///         Creates a Generator delegate.
        ///     </para>
        /// </summary>
        /// <param name="fn"></param>
        /// <typeparam name="T"></typeparam>
        public static Generator<T> Expression<T>(Func<Generator, T> fn) {
            var generator = new Generator();
            return () => generator.Generate(fn);
        }

        /// <summary>
        ///     <para>
        ///         Creates a Generator delegate using the configurement.
        ///     </para>
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="config"></param>
        /// <typeparam name="T"></typeparam>
        public static Generator<T> Expression<T>(Func<Generator, T> fn, Configurement config) {
            var generator = new Generator(config);
            return () => generator.Generate(fn);
        }
    }
}