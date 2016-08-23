using System.Collections.Generic;
using System.Text;

namespace DataGen.Types {
    public abstract class Unique<T> {
        protected int AttemptLimit { get; }
        private HashSet<T> HashSet { get; }
        // ReSharper disable once StaticMemberInGenericType
        protected static readonly StringBuilder Builder = new StringBuilder();

        protected Unique(int attemptLimit) {
            AttemptLimit = attemptLimit;
            HashSet = new HashSet<T>();
        }

        protected bool ClearValidateSave(T item) {
            Builder.Clear();
            if (HashSet.Contains(item)) return false;
            HashSet.Add(item);
            return true;
        }
    }
}