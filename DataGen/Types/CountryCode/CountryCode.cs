using System.Linq;

namespace DataGen.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    public class CountryCode {
        public string Name { get; }
        private string Code { get; }

        // ReSharper disable once InconsistentNaming
        public CountryCode(string name , string code) {
            Name = name;
            Code = code;
        }

    }
}