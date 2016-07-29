using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen.Types.Name
{
        // Is generated from json
        // ReSharper disable once ClassNeverInstantiated.Global
        public class NameRepository {
            public NameRepository(IEnumerable<string> femaleFirstNames, IEnumerable<string> maleFirstNames,
                IEnumerable<string> lastNames, string country, string region) {
                FemaleFirstNames = femaleFirstNames;
                MaleFirstNames = maleFirstNames;
                LastNames = lastNames;
                Origin = new Origin(country, region);
            }

            public IEnumerable<string> FemaleFirstNames { get; }
            public IEnumerable<string> LastNames { get; }
            public IEnumerable<string> MaleFirstNames { get; }
            public Origin Origin { get; }

            public IEnumerable<string> MixedFirstNames
                => FemaleFirstNames.Concat(MaleFirstNames);
        }

        public class Origin {
            public readonly string Country;
            public readonly string Region;

            public Origin(string country, string region) {
                Country = country;
                Region = region;
            }
        }
}
