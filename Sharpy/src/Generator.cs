﻿using System;
using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using Sharpy.Properties;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Contains various methods for generating data and utility methods which you can combine with the generation
    ///         methods.
    ///         If you would want the same result every time you invoke these methods you can set the seed for the random
    ///         required
    ///         by the constructor.
    ///         If want to add your own methods you can derive from this class.
    ///     </para>
    ///     <para>If you want to map this to instantiate another class you can call Generate/GenerateSequence.</para>
    ///     <para>For examples please visit https://inputfalken.github.io/Sharpy/ </para>
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
        ///     <para>Returns a generator with your configurement</para>
        /// </summary>
        /// <param name="configurement"></param>
        /// <returns></returns>
        public Generator(Configurement configurement) {
            _random = configurement.Random;
            _doubleProvider = configurement.DoubleProvider;
            _integerProvider = configurement.IntegerProvider;
            _longProvider = configurement.LongProvider;
            _nameProvider = configurement.NameProvider;
            _dateGenerator = new DateGenerator(configurement.Random);
            _mailbuilder = new EmailBuilder(configurement.MailDomains, configurement.Random);
            _socialSecurityNumberGenerator = new SecurityNumberGen(configurement.Random);
            _numberGenerator = new NumberGenerator(configurement.Random);
            _uniqueNumbers = configurement.UniqueNumbers;
        }

        /// <summary>
        ///     <para>Returns a generator which will Randomize the same result by the seed.</para>
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Generator(int seed) : this(new Configurement(seed)) {}

        /// <summary>
        ///     <para>Returns a generator which will randomize with the random supplied.</para>
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public Generator(Random random) : this(new Configurement(random)) {}

        /// <summary>
        ///     <para>Returns a generator which will randomize new results every time program is executed.</para>
        /// </summary>
        /// <returns></returns>
        public Generator() : this(new Configurement()) {}

        /// <summary>
        ///     <para>Generates a double.</para>
        /// </summary>
        /// <returns></returns>
        public double Double() => _doubleProvider.Double();

        /// <summary>
        ///     <para>Generates a double within max value.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double max) => _doubleProvider.Double(max);

        /// <summary>
        ///     <para>Generates a within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <summary>
        ///     <para>Generates a integer.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <summary>
        ///     <para>Generates a integer within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <summary>
        ///     <para>Generates a integer.</para>
        /// </summary>
        /// <returns></returns>
        public int Integer() => _integerProvider.Integer();

        /// <summary>
        ///     <para>Generates a long within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <summary>
        ///     <para>Generates a long within max.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long max) => _longProvider.Long(max);

        /// <summary>
        ///     Generates a long.
        /// </summary>
        /// <returns></returns>
        public long Long() => _longProvider.Long();


        /// <summary>
        ///     <para>Returns a string representing a first name.</para>
        /// </summary>
        /// <returns></returns>
        public string FirstName() => _nameProvider.FirstName();

        /// <summary>
        ///     <para>Returns a string representing a first name based on Gender.</para>
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public string FirstName(Gender gender) => _nameProvider.FirstName(gender);

        /// <summary>
        ///     <para>Returns a string representing a last name.</para>
        /// </summary>
        /// <returns></returns>
        public string LastName() => _nameProvider.LastName();

        /// <summary>
        ///     <para>Randomizes one of the arguments.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T Params<T>(params T[] items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>Randomizes one of the elements in the IReadOnlyList.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>Randomizes a bool.</para>
        /// </summary>
        /// <returns></returns>
        public bool Bool() => _random.Next(2) != 0;

        /// <summary>
        ///     <para>Randomizes a date based on age.</para>
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public LocalDate DateByAge(int age) => _dateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     <para>Randomizes a date based on year.</para>
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public LocalDate DateByYear(int year) => _dateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     <para>Randomizes a unique SocialSecurity Number.</para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated"></param>
        /// <returns></returns>
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
        ///     <para>Returns a string representing a mailaddress.</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string MailAddress(string name, string secondName = null)
            => _mailbuilder.Mail(name, secondName);

        /// <summary>
        ///     <para>Returns a number with the length of the argument.</para>
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
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
        ///     <para>Returns a random username from a huge collection.</para>
        /// </summary>
        /// <returns></returns>
        public string UserName() => _lazyUsernames.Value.RandomItem(_random);

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();
    }
}