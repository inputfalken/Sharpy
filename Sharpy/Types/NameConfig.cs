using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Types.Name;
using Sharpy.Types.String;

namespace Sharpy.Types {
    /// <summary>
    /// 
    /// </summary>
    public class NameConfig : IStringFilter<NameConfig> {
        private static Lazy<Filter<Name.Name>> LazyNameFilter { get; } =
            new Lazy<Filter<Name.Name>>(
                () => new Filter<Name.Name>(JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                    Encoding.UTF8.GetString(Properties.Resources.NamesByOrigin))));


        private Filter<Name.Name> _nameFilter;

        internal Filter<Name.Name> NameFilter {
            get { return _nameFilter ?? LazyNameFilter.Value; }
            private set { _nameFilter = value; }
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public NameConfig NameOrigin(params Country[] countries) {
            NameFilter = ByCountry(countries);
            return this;
        }

        private Filter<Name.Name> ByCountry(params Country[] args)
            => new Filter<Name.Name>(NameFilter.Where(name => args.Contains(name.Country)));


        private Filter<Name.Name> ByRegion(params Region[] args)
            => new Filter<Name.Name>(NameFilter.Where(name => args.Contains(name.Region)));


        internal Filter<Name.Name> ByType(NameType nameType) {
            switch (nameType) {
                case NameType.FemaleFirstName:
                    return new Filter<Name.Name>(NameFilter.Where(name => name.Type == 1));
                case NameType.MaleFirstName:
                    return new Filter<Name.Name>(NameFilter.Where(name => name.Type == 2));
                case NameType.LastName:
                    return new Filter<Name.Name>(NameFilter.Where(name => name.Type == 3));
                case NameType.MixedFirstName:
                    return new Filter<Name.Name>(NameFilter.Where(name => name.Type == 1 | name.Type == 2));
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }

        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public NameConfig NameOrigin(params Region[] regions) {
            NameFilter = ByRegion(regions);
            return this;
        }

        private static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotStartWith(string arg) {
            NameFilter = new Filter<Name.Name>(NameFilter.Where(s => IndexOf(s.Data, arg) != 0));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotContain(string arg) {
            NameFilter = new Filter<Name.Name>(NameFilter.Where(s => IndexOf(s.Data, arg) < 0));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig StartsWith(params string[] args) {
            NameFilter = args.Length == 1
                ? new Filter<Name.Name>(NameFilter.Where(s => IndexOf(s.Data, args[0]) == 0))
                : new Filter<Name.Name>(NameFilter.Where(s => args.Any(arg => IndexOf(s.Data, arg) == 0)));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig Contains(params string[] args) {
            NameFilter = args.Length == 1
                ? new Filter<Name.Name>(NameFilter.Where(s => s.Data.IndexOf(args[0], StringComparison.OrdinalIgnoreCase) >= 0))
                :  new Filter<Name.Name>(NameFilter.Where(s => args.Any(s.Data.Contains)));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public NameConfig ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            NameFilter = new Filter<Name.Name>(NameFilter.Where(s => s.Data.Length == length));
            return this;
        }
    }
}