using System.Linq;

namespace DataGen.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    public class CountryCode {
        public string Name { get; }
        private string DialCode { get; }
        public string Code { get; }

        // ReSharper disable once InconsistentNaming
        public CountryCode(string name, string dial_Code, string code) {
            Name = name;
            DialCode = dial_Code;
            Code = code;
        }

        public string ConstructNumber => Enumerable.Range(1, 7)
            .Aggregate(DialCode, (current, i) => current + HelperClass.Randomizer(0, 9));
    }
}