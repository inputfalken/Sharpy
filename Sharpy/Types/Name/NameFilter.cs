using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Enums;
using Sharpy.Types.String;

namespace Sharpy.Types.Name {
    /// <summary>
    /// 
    /// </summary>
    internal sealed class NameFilter : Filter<Name> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        internal NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
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