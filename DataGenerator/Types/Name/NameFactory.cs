using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name
{
    internal class NameFactory
    {
        public NameFactory(IGenerator<string> generator) {
            Generator = generator;
            NameData = JsonConvert.DeserializeObject<NameData>(File.ReadAllText("Data/Types/Name/data.json"));
        }

        private IGenerator<string> Generator { get; }

        private NameData NameData { get; }

        //Create another to return a list with many names with same requirement
        public Func<Gender, string> GetFirstName(CountryEnum countryEnum) => gender => {
            switch (countryEnum) {
                case CountryEnum.Sweden:
                    var sweden =
                        NameData.Regions.First(region => region.Name == "europe")
                            .Countries.First(country => country.Name == "sweden");
                    return
                        Generator.Generate(gender == Gender.Female
                            ? sweden.CommonName.Female
                            : sweden.CommonName.Male);
                case CountryEnum.Norway:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
            }
        };


        public string GetLastName() {
            return "lastName";
        }
    }
}