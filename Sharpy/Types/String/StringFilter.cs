using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy.Types.String {
    /// <summary>
    /// 
    /// </summary>
    public sealed class StringFilter : IStringFilter<StringFilter> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        public StringFilter(IEnumerable<string> enumerable) {
            Filter = new Filter<string>(enumerable);
        }

        internal Filter<string> Filter { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public StringFilter DoesNotStartWith(string arg) => new StringFilter(Filter.Where(s => IndexOf(s, arg) != 0));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public StringFilter DoesNotContain(string arg) => new StringFilter(Filter.Where(s => IndexOf(s, arg) < 0));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public StringFilter StartsWith(params string[] args)
            => args.Length == 1
                ? new StringFilter(Filter.Where(s => IndexOf(s, args[0]) == 0))
                : new StringFilter(Filter.Where(s => args.Any(arg => IndexOf(s, arg) == 0)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public StringFilter Contains(params string[] args)
            => args.Length == 1
                ? new StringFilter(Filter.Where(s => IndexOf(s, args[0]) >= 0))
                : new StringFilter(Filter.Where(s => args.Any(arg => IndexOf(s, arg) >= 0)));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public StringFilter ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            return new StringFilter(Filter.Where(s => s.Length == length));
        }

        private static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);
    }
}