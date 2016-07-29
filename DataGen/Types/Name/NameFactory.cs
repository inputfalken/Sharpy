using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGen.Types.Name {
    public static class NameFactory {


        /// <summary>
        /// Takes a function which will collect from specified countries/country. The function's argument is a NameRepository containing IEnumerable of different name types.
        /// </summary>
        /// <param name="func">A function who's argument will be a NameRepository for the selected countries</param>
        /// <param name="countries">Lets you decide which name type you are interested in</param>
        /// <returns></returns>
        public static IEnumerable<string> Collection(Func<NameRepository, IEnumerable<string>> func,
            params string[] countries) {
            var list = new List<string>();
            foreach (var country in countries)
                list.AddRange(func(NameRepository.FilterByCountry(country)));
            return Filter.RepeatedData(list);
        }

        /// <summary>
        /// Takes a function which will collect from specified regions/region. The function's argument is a NameRepository containing IEnumerable of different name types.
        /// </summary>
        /// <param name="func">A function who's argument will be a NameRepository for the selected countries</param>
        /// <param name="regions">Lets you decide which name type you are interested in</param>
        /// <returns></returns>
        public static IEnumerable<string> Collection(Func<NameRepository, IEnumerable<string>> func,
            params Region[] regions) {
            var list = new List<string>();
            foreach (var region in regions)
                foreach (var nameRepository in NameRepository.FilterByRegion(region))
                    list.AddRange(func(nameRepository));
            return Filter.RepeatedData(list);
        }

        /// <summary>
        /// Takes a function which will collect all avaiable data. The function's argument is a NameRepository containing IEnumerable of different name types.
        /// </summary>
        /// <param name="func">Lets you decide which name type you are interested in</param>
        /// <returns></returns>
        public static IEnumerable<string> Collection(Func<NameRepository, IEnumerable<string>> func) {
            var list = new List<string>();
            foreach (var nameRepository in NameRepository.NameRepositories)
                list.AddRange(func(nameRepository));
            return Filter.RepeatedData(list);
        }

    }
}