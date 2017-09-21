using System;
using System.Collections.Generic;

namespace Sharpy.Implementation {
    /// <summary>
    /// Provides <see cref="ISet{T}"/> and <see cref="System.Random"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UniqueRandomizer<T> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        protected UniqueRandomizer(Random random) {
            Random = random;
            HashSet = new HashSet<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        protected Random Random { get; }

        /// <summary>
        ///     This <see cref="ISet{T}" /> will be the history of all the created elements in derived classes
        /// </summary>
        protected ISet<T> HashSet { get; }
    }
}