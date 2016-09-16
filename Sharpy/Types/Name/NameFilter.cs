using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Types.Enums;
using Sharpy.Types.String;

namespace Sharpy.Types.Name {
    /// <summary>
    /// 
    /// </summary>
    public sealed class NameFilter : Filter<Name>, IStringFilter<NameFilter> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        internal NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameFilter DoesNotStartWith(string arg) => new NameFilter(this.Where(s => IndexOf(s.Data, arg) != 0));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameFilter DoesNotContain(string arg) => new NameFilter(this.Where(s => IndexOf(s.Data, arg) < 0));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameFilter StartsWith(params string[] args)
            => args.Length == 1
                ? new NameFilter(this.Where(s => IndexOf(s.Data, args[0]) == 0))
                : new NameFilter(this.Where(s => args.Any(arg => IndexOf(s.Data, arg) == 0)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameFilter Contains(params string[] args) {
            return args.Length == 1
                ? new NameFilter(this.Where(s => s.Data.IndexOf(args[0], StringComparison.OrdinalIgnoreCase) >= 0))
                : new NameFilter(this.Where(s => args.Any(s.Data.Contains)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public NameFilter ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            return new NameFilter(this.Where(s => s.Data.Length == length));
        }


        internal NameFilter ByCountry(params Country[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Country)));


        internal NameFilter ByRegion(params Region[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Region)));


        internal NameFilter ByType(NameType nameType) {
            switch (nameType) {
                case NameType.FemaleFirstName:
                    return new NameFilter(this.Where(name => name.Type == 1));
                case NameType.MaleFirstName:
                    return new NameFilter(this.Where(name => name.Type == 2));
                case NameType.LastName:
                    return new NameFilter(this.Where(name => name.Type == 3));
                case NameType.MixedFirstName:
                    return new NameFilter(this.Where(name => name.Type == 1 | name.Type == 2));
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }
    }
}