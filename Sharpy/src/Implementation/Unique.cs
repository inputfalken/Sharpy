using System;
using System.Collections.Generic;

namespace Sharpy.Implementation {
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class Unique<T> {
        // ReSharper disable once StaticMemberInGenericType
        /// <summary>
        /// </summary>
        //protected static readonly StringBuilder Builder = new StringBuilder();
        /// <summary>
        /// </summary>
        /// <param name="random"></param>
        protected Unique(Random random) {
            Random = random;
            HashSet = new HashSet<T>();
        }


        /// <summary>
        /// </summary>
        protected Random Random { get; }

        /// <summary>
        ///     This hashset will be the history of all the created elements in derived classes
        /// </summary>
        protected HashSet<T> HashSet { get; }

        //        Builder.Append(s);
        //    foreach (var s in strings)

        //protected static string Build(params string[] strings) {
        //    var str = Builder.ToString();
        //    Builder.Clear();
        //    return str;
        //}
    }
}