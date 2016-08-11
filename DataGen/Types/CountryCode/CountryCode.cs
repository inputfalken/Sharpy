using System.Linq;

namespace DataGen.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    public class CountryCode {
        public string Name { get; }
        private string Code { get; }

        public CountryCode(string name, string code) {
            Name = name;
            Code = code;
        }

        public string RandomPhoneNumber(int length, string preNumber = null)
            => Enumerable.Range(1, length)
                .Aggregate(Code + preNumber, (current, i) => current + HelperClass.Randomizer(0, 9));

        public string RandomPhoneNumber(int minLength, int maxLength, string preNumber = null)
            => Enumerable.Range(1, HelperClass.Randomizer(minLength, maxLength))
                .Aggregate(Code + preNumber, (current, i) => current + HelperClass.Randomizer(0, 9));
    }
}