using System;
using NodaTime;

namespace Sharpy.Randomizer.Generators {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<long> {
        private readonly int _length;

        internal NumberGenerator(Random random, int length, bool unique = false)
            : base(random) {
            _length = length;
            Unique = unique;
            Min = (int) Math.Pow(10, length - 1);
            Max = Min*10 - 1;
        }

        private bool Unique { get; }
        private int Min { get; }
        private int Max { get; }

        internal long RandomNumber() {
            var next = Random.Next(Min, Max);
            return Unique ? CreateUniqueNumber(next, OnDuplicate) : next;
        }


        private long CreateUniqueNumber(long startNumber, Func<long, long> func) {
            var number = func(startNumber);
            while (HashSet.Contains(number)) number = func(number);
            HashSet.Add(number);
            return number;
        }

        internal long SocialSecurity(LocalDate date) {
            var month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var year = date.YearOfCentury < 10 ? $"0{date.YearOfCentury}" : date.YearOfCentury.ToString();
            var day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var controlNumber = Random.Next(Min, Max);
            var securityNumber = CreateUniqueNumber(long.Parse(Build(year, month, day, controlNumber.ToString())),
                OnDuplicate);
            return securityNumber;
        }

        protected override long OnDuplicate(long item) {
            if (item == Max) item = Min;
            else item++;
            return item;
        }

        public override string ToString() => $"Length: {_length}, Unique: {Unique}";
    }
}