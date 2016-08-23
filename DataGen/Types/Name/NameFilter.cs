using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.String;

namespace DataGen.Types.Name {
    public sealed class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }


        public NameFilter ByCountry(params string[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Country)));


        public NameFilter ByCountry(bool uniqueNames, params string[] args) {
            if (!uniqueNames) return new NameFilter(this.Where(name => args.Contains(name.Country)));
            var names = new List<Name>();
            foreach (var name in this)
                if (args.Contains(name.Country) && names.All(name1 => name1.Data != name.Data)) names.Add(name);
            return new NameFilter(names);
        }

        public NameFilter ByRegion(params string[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Region)));


        public NameFilter ByType(NameTypes nameTypes) {
            switch (nameTypes) {
                case NameTypes.FemaleFirst:
                    return new NameFilter(this.Where(name => name.Type == 1));
                case NameTypes.MaleFirst:
                    return new NameFilter(this.Where(name => name.Type == 2));
                case NameTypes.LastNames:
                    return new NameFilter(this.Where(name => name.Type == 3));
                case NameTypes.MixedFirstNames:
                    return new NameFilter(this.Where(name => name.Type == 1 | name.Type == 2));
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameTypes), nameTypes, null);
            }
        }
    }

    public enum NameTypes {
        FemaleFirst,
        MaleFirst,
        LastNames,
        MixedFirstNames
    }
}