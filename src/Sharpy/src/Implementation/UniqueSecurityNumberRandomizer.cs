using System;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    internal class UniqueSecurityNumberRandomizer : Unique<long>, ISecurityNumberProvider {
        internal UniqueSecurityNumberRandomizer(Random random) : base(random) { }

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

        public string SecurityNumber(DateTime date, bool formated = true) {
            var result = SecurityNumber(FormatDigit(date.Year % 100)
                    .Append(FormatDigit(date.Month), FormatDigit(date.Day)))
                .ToString();
            var securityNumber = result;
            if (securityNumber.Length != 10)
                securityNumber = result.Prefix(10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        private static string FormatDigit(int i) => i < 10 ? i.Prefix(1) : i.ToString();
    }
}