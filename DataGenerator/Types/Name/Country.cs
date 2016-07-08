namespace DataGenerator.Types.Name
{
    internal class Country
    {
        public Country(string name, CommonName commonName) {
            Name = name;
            CommonName = commonName;
        }

        public string Name { get; }
        public CommonName CommonName { get; }
    }
}