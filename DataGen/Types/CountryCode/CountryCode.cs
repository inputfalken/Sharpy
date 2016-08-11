namespace DataGen.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    public class CountryCode {
        public string Name { get; }
        public string DialCode { get; }
        public string Code { get; }

        public CountryCode(string name, string dialCode, string code) {
            Name = name;
            DialCode = dialCode;
            Code = code;
        }
    }
}