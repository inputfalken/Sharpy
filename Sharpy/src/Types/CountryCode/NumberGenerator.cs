using System;
using NodaTime;

namespace Sharpy.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<int> {
        internal NumberGenerator(Random random, int length, string prefix,
            bool unique = false)
            : base(random) {
            Prefix = prefix;
            Unique = unique;
            Min = (int) Math.Pow(10, length - 1);
            Max = Min*10 - 1;
        }

        private string Prefix { get; }
        private bool Unique { get; }
        private int Min { get; }
        private int Max { get; }

        internal string RandomNumber() {
            var next = Random.Next(Min, Max);
            if (!Unique) return Build(Prefix, next.ToString());
            while (HashSet.Contains(next)) {
                if (next == Max)
                    next = Min;
                next++;
            }
            HashSet.Add(next);
            return Build(Prefix, next.ToString());
        }

        private static string Build(params string[] strings) {
            foreach (var s in strings)
                Builder.Append(s);
            var str = Builder.ToString();
            Builder.Clear();
            return str;
        }
    }

    /// <summary>
    ///  Very similiar to NumberGenerator, maybe could make more dry...
    /// </summary>
    internal class SocialSecurityGenerator : Unique<long> {
        public SocialSecurityGenerator(Random random, int length) : base(random) {
            Min = (int) Math.Pow(10, length - 1);
            Max = Min*10 - 1;
        }

        private int Max { get; }

        private int Min { get; }

        internal string Generate(LocalDate date) {
            var controlNumber = Random.Next(Min, Max);
            var month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var year = date.YearOfCentury < 10 ? $"0{date.YearOfCentury}" : date.YearOfCentury.ToString();
            var day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var securityNumber = long.Parse(year + month + day + controlNumber);
            while (HashSet.Contains(securityNumber)) {
                if (controlNumber == Max) controlNumber = Min;
                else controlNumber++;
                securityNumber = long.Parse(year + month + day + controlNumber);
            }
            HashSet.Add(securityNumber);
            return $"{year}{month}{day}-{controlNumber}";
        }
    }
}