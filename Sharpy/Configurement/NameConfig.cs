using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Properties;
using Sharpy.Types;
using Sharpy.Types.Name;
using Sharpy.Types.String;

namespace Sharpy.Configurement {
    /// <summary>
    ///     Is Responsible for filtering the names
    /// </summary>
    public sealed class NameConfig : IStringFilter<NameConfig> {
        private Fetcher<Name> _fetcher;

        private static Lazy<Fetcher<Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name>>(
                () => new Fetcher<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Fetcher<Name> Fetcher {
            get { return _fetcher ?? LazyNames.Value; }
            private set { _fetcher = value; }
        }

        public NameConfig FilterNameByString(Func<string, bool> predicate) {
            Fetcher = new Fetcher<Name>(Fetcher.Where(name => predicate(name.Data)));
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotStartWith(string arg) {
            Fetcher = new Fetcher<Name>(Fetcher.Where(s => IndexOf(s.Data, arg) != 0));
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotContain(string arg) {
            Fetcher = new Fetcher<Name>(Fetcher.Where(s => IndexOf(s.Data, arg) < 0));
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig StartsWith(params string[] args) {
            Fetcher = args.Length == 1
                ? new Fetcher<Name>(Fetcher.Where(s => IndexOf(s.Data, args[0]) == 0))
                : new Fetcher<Name>(Fetcher.Where(s => args.Any(arg => IndexOf(s.Data, arg) == 0)));
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig Contains(params string[] args) {
            Fetcher = args.Length == 1
                ? new Fetcher<Name>(
                    Fetcher.Where(s => s.Data.IndexOf(args[0], StringComparison.OrdinalIgnoreCase) >= 0))
                : new Fetcher<Name>(Fetcher.Where(s => args.Any(s.Data.Contains)));
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public NameConfig ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            Fetcher = new Fetcher<Name>(Fetcher.Where(s => s.Data.Length == length));
            return this;
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public NameConfig Origin(params Country[] countries) {
            Fetcher = ByCountry(countries);
            return this;
        }


        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public NameConfig Origin(params Region[] regions) {
            Fetcher = ByRegion(regions);
            return this;
        }

        private static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);

        private Fetcher<Name> ByCountry(params Country[] args)
            => new Fetcher<Name>(Fetcher.Where(name => args.Contains(name.Country)));


        private Fetcher<Name> ByRegion(params Region[] args)
            => new Fetcher<Name>(Fetcher.Where(name => args.Contains(name.Region)));


        internal Fetcher<Name> Type(NameType nameType) {
            switch (nameType) {
                case NameType.FemaleFirstName:
                    return new Fetcher<Name>(Fetcher.Where(name => name.Type == 1));
                case NameType.MaleFirstName:
                    return new Fetcher<Name>(Fetcher.Where(name => name.Type == 2));
                case NameType.LastName:
                    return new Fetcher<Name>(Fetcher.Where(name => name.Type == 3));
                case NameType.MixedFirstName:
                    return new Fetcher<Name>(Fetcher.Where(name => name.Type == 1 | name.Type == 2));
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }
    }
}