using System.Collections.Generic;
using System.Text;

namespace DataGen.Types {
    public abstract class Unique<T> {
        // ReSharper disable once StaticMemberInGenericType
        protected static readonly StringBuilder Builder = new StringBuilder();

        protected Unique(int attemptLimit) {
            AttemptLimit = attemptLimit;
            HashSet = new HashSet<T>();
        }

        protected int AttemptLimit { get; }

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