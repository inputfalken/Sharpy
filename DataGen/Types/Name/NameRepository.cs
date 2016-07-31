using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataGen.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NameRepository {
        //Todo rework json file and this object
        public NameRepository(IEnumerable<string> femaleFirstNames, IEnumerable<string> maleFirstNames,
            IEnumerable<string> lastNames, string country, string region) {
            FemaleFirstNames = femaleFirstNames;
            MaleFirstNames = maleFirstNames;
            LastNames = lastNames;
            _origin = new Origin(country, region);
        }

        public IEnumerable<string> FemaleFirstNames { get; }
        public IEnumerable<string> LastNames { get; }
        public IEnumerable<string> MaleFirstNames { get; }

        public IEnumerable<string> MixedFirstNames
            => FemaleFirstNames.Concat(MaleFirstNames);

        public readonly Origin _origin;


        public class Origin {
            public readonly string Country;
            public readonly string Region;

            public Origin(string country, string region) {
                Country = country;
                Region = region;
            }
        }
    }
}