using System;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    public class UniqueSecurityNumberRandomizer : Unique<long> {
        protected UniqueSecurityNumberRandomizer(Random random) : base(random) { }

        private long SecurityNumber(string dateNumber) {
            var controlNumber = Random.Next(10000);
            var number = long.Parse(dateNumber + controlNumber);
            var resets = 0;
            while (HashSet.Contains(number)) {
                if (controlNumber < 9999) {
                    controlNumber++;
                }
                else {
                    controlNumber = 0;
                    if (resets++ == 2)
                        throw new Exception("You have reached the maximum possible combinations for a control number");
                }
                number = long.Parse(dateNumber.Append(controlNumber));
            }
            HashSet.Add(number);
            return number;
        }

        protected string SecurityNumber(DateTime date, bool formated) {
            var result = SecurityNumber(FormatDigit(date.Year % 100)
                    .Append(FormatDigit(date.Month), FormatDigit(date.Day)))
                .ToString();
            var securityNumber = result;
            if (securityNumber.Length != 10)
                securityNumber = result.Prefix(10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        protected DateTime RandomizeDate() {
            var dateTime = DateTime.Now;
            var year = Random.Next(1900, dateTime.Year);
            var month = year == dateTime.Year ? Random.Next(0, dateTime.Month) : Random.Next(1, 13);
            var date = year == dateTime.Year && month == dateTime.Month
                ? Random.Next(1, dateTime.Day + 1)
                : Random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, date);
        }

        private static string FormatDigit(int i) => i < 10 ? i.Prefix(1) : i.ToString();
    }

    public sealed class UniqueFormattedSecurityRandomizer : UniqueSecurityNumberRandomizer, ISecurityNumberProvider {
        internal UniqueFormattedSecurityRandomizer(Random random) : base(random) { }
        public string SecurityNumber(DateTime date) => SecurityNumber(date, true);

        public string SecurityNumber() => SecurityNumber(RandomizeDate());
    }

    public sealed class UniqueSecurityRandomizer : UniqueSecurityNumberRandomizer, ISecurityNumberProvider {
        internal UniqueSecurityRandomizer(Random random) : base(random) { }
        public string SecurityNumber(DateTime date) => SecurityNumber(date, false);

        public string SecurityNumber() => SecurityNumber(RandomizeDate());
    }
}