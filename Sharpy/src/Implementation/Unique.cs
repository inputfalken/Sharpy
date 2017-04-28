using System;
using System.Collections.Generic;

namespace Sharpy.Implementation {
    internal abstract class Unique<T> {
        protected Unique(Random random) {
            Random = random;
            HashSet = new HashSet<T>();
        }

        protected Random Random { get; }

        /// <summary>
        ///     This hashset will be the history of all the created elements in derived classes
        /// </summary>
        protected ISet<T> HashSet { get; }
    }
}