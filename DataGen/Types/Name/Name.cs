namespace DataGen.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Name {

        public Name(int type, string country, string region, string name) {
            Country = country;
            Region = region;
            Data = name;
            Type = type;
        }

        public int Type { get; }
        public string Data { get; }
        public string Region { get; }
        public string Country { get; }
        public override string ToString() => Data;
    }
}