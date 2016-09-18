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
        private static Lazy<NameFilter> LazyNameFilter { get; } =
            new Lazy<NameFilter>(() => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                Encoding.UTF8.GetString(Properties.Resources.NamesByOrigin))));

        private NameFilter _nameFilter;

        internal NameFilter NameFilter {
            get { return _nameFilter ?? LazyNameFilter.Value; }
            private set { _nameFilter = value; }
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public NameConfig NameOrigin(params Country[] countries) {
            NameFilter = NameFilter.ByCountry(countries);
            return this;
        }

        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public NameConfig NameOrigin(params Region[] regions) {
            NameFilter = NameFilter.ByRegion(regions);
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
            NameFilter = new NameFilter(NameFilter.Where(s => IndexOf(s.Data, arg) != 0));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotContain(string arg) {
            NameFilter = new NameFilter(NameFilter.Where(s => IndexOf(s.Data, arg) < 0));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig StartsWith(params string[] args) {
            NameFilter = args.Length == 1
                ? new NameFilter(NameFilter.Where(s => IndexOf(s.Data, args[0]) == 0))
                : new NameFilter(NameFilter.Where(s => args.Any(arg => IndexOf(s.Data, arg) == 0)));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig Contains(params string[] args) {
            NameFilter = args.Length == 1
                ? new NameFilter(NameFilter.Where(s => s.Data.IndexOf(args[0], StringComparison.OrdinalIgnoreCase) >= 0))
                : new NameFilter(NameFilter.Where(s => args.Any(s.Data.Contains)));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public NameConfig ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            NameFilter = new NameFilter(NameFilter.Where(s => s.Data.Length == length));
            return this;
        }
    }
}