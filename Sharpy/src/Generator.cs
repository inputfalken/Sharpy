using System;
using System.Collections.Generic;
using System.Threading;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.Implementation.Generators;

namespace Sharpy {
    /// <summary>
    ///     <para>Static generator using my implementation of IGenerator</para>
    ///     <para>Can also create instances of the generator</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    /// <returns></returns>
    public sealed class Generator : IGenerator<StringType> {
        private Tuple<int, int, int> _phoneState;

        static Generator() {
            StaticGen = Create();
            Configurement = StaticGen.Config;
        }

        /// <summary>
        ///     <para>Configures Generator.</para>
        /// </summary>
        public Config Config { get; } = new Config();

        private static Generator StaticGen { get; }

        /// <summary>
        ///     <para>Configures Generator.</para>
        /// </summary>
        public static Config Configurement { get; }


        T IGenerator<StringType>.Params<T>(params T[] items) => items[Config.Random.Next(items.Length)];

        T IGenerator<StringType>.CustomCollection<T>(IList<T> items) => items[Config.Random.Next(items.Count)];

        string IGenerator<StringType>.String(StringType type) {
            if (!Config.Dictionary.ContainsKey(type))
                Config.Dictionary.Add(type, new Fetcher<string>(Config.StringType(type)));
            return Config.Dictionary[type].RandomItem(Config.Random);
        }

        bool IGenerator<StringType>.Bool() => Config.Random.Next(2) != 0;

        int IGenerator<StringType>.Integer(int max) {
            IGenerator<StringType> foo = this;
            return foo.Integer(0, max);
        }

        int IGenerator<StringType>.Integer(int min, int max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return Config.Random.Next(min, max);
        }

        int IGenerator<StringType>.Integer() => Config.Random.Next(int.MinValue, int.MaxValue);

        LocalDate IGenerator<StringType>.DateByAge(int age) => Config.DateGenerator.RandomDateByAge(age);

        LocalDate IGenerator<StringType>.DateByYear(int year) => Config.DateGenerator.RandomDateByYear(year);

        string IGenerator<StringType>.SocialSecurityNumber(LocalDate date, bool formated) {
            var securityNumber = Config
                .SocialSecurityNumberGenerator
                .SecurityNumber(Config.Random.Next(1000, 9999),
                    FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)))
                .ToString();
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        private static string Prefix(string str, int ammount) => new string('0', ammount).Append(str);

        private static string FormatDigit(int i) => i < 10 ? "0".Append(i) : i.ToString();

        string IGenerator<StringType>.MailAddress(string name, string secondName)
            => Config.Mailgen.Mail(name, secondName);

        // Currently The combinations possible is (10^(length -1) * 0.9).
        string IGenerator<StringType>.PhoneNumber(int length, string prefix) {
            //If the field _phoneState not null and length inside phonestate is not changed.
            if (_phoneState != null && _phoneState.Item1 == length)
                return prefix + Config.PhoneNumberGenerator.RandomNumber(_phoneState.Item2, _phoneState.Item3, true);

            //Else assign new value to _phoneState.
            var min = (int) Math.Pow(10, length - 1);
            var max = min*10 - 1;
            _phoneState = new Tuple<int, int, int>(length, min, max);
            return prefix + Config.PhoneNumberGenerator.RandomNumber(_phoneState.Item2, _phoneState.Item3, true);
        }

        long IGenerator<StringType>.Long(long min, long max) => Config.Random.NextLong(min, max);

        long IGenerator<StringType>.Long(long max) => Config.Random.NextLong(max);

        long IGenerator<StringType>.Long() => Config.Random.NextLong();

        double IGenerator<StringType>.Double() => Config.Random.NextDouble();

        double IGenerator<StringType>.Double(double max) => Config.Random.NextDouble(max);

        double IGenerator<StringType>.Double(double min, double max) => Config.Random.NextDouble(min, max);

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
        public override string ToString() => $"Configurement for Generator \n{Config}";
    }
}