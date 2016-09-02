using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.String;

namespace DataGen.Types.Name {
    public sealed class NameFilter : Filter<Name>, IStringFilter<NameFilter> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }


        public NameFilter ByCountry(params string[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Country)));


        public NameFilter ByRegion(params string[] args)
            => new NameFilter(this.Where(name => args.Contains(name.Region)));


        internal NameFilter ByType(NameTypes nameTypes) {
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

        public NameFilter DoesNotStartWith(string arg) => new NameFilter(this.Where(s => IndexOf(s.Data, arg) != 0));

        public NameFilter DoesNotContain(string arg) => new NameFilter(this.Where(s => !s.Data.Contains(arg)));

        public NameFilter StartsWith(params string[] args)
            => args.Length == 1
                ? new NameFilter(this.Where(s => IndexOf(s.Data, args[0]) == 0))
                : new NameFilter(this.Where(s => args.Any(arg => IndexOf(s.Data, arg) == 0)));


        public NameFilter Contains(params string[] args)
            => args.Length == 1
                ? new NameFilter(this.Where(s => s.Data.Contains(args[0])))
                : new NameFilter(this.Where(s => args.Any(s.Data.Contains)));

        public NameFilter ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            return new NameFilter(this.Where(s => s.Data.Length == length));
        }
    }

    public enum NameTypes {
        FemaleFirst,
        MaleFirst,
        LastNames,
        MixedFirstNames
    }
}