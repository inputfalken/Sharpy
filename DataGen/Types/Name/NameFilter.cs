using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public sealed class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }


        public NameFilter ByCountry(params string[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Country)));

        public NameFilter ByRegion(params string[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Region)));

        public NameFilter FemaleFirstNames => new NameFilter(this.Where(name => name.Type == 1));
        public NameFilter MaleFirstNames => new NameFilter(this.Where(name => name.Type == 2));
        public NameFilter LastNames => new NameFilter(this.Where(name => name.Type == 3));
        public NameFilter MixedFirstNames => new NameFilter(this.Where(name => name.Type == 1 || name.Type == 2));
    }
}