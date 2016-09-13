using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpy.Types {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Unique<T> {
        // ReSharper disable once StaticMemberInGenericType
        /// <summary>
        /// 
        /// </summary>
        protected static readonly StringBuilder Builder = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attemptLimit"></param>
        /// <param name="random"></param>
        protected Unique(int attemptLimit, Random random) {
            AttemptLimit = attemptLimit;
            Random = random;
            HashSet = new HashSet<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        protected int AttemptLimit { get; }
        /// <summary>
        /// 
        /// </summary>
        protected Random Random { get;  }

        /// <summary>
        ///     This hashset will be the history of all the created elements in derived classes
        /// </summary>
        private HashSet<T> HashSet { get; }

        /// <summary>
        ///     This method is used in order to validate if the created element is unique
        /// </summary>
        protected bool ClearValidateSave(T item) {
            Builder.Clear();
            if (HashSet.Contains(item)) return false;
            HashSet.Add(item);
            return true;
        }
    }
}