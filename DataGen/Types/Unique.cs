using System.Collections.Generic;
using System.Text;

namespace DataGen.Types {
    public abstract class Unique<T> {
        private HashSet<T> HashSet { get; }
        // ReSharper disable once StaticMemberInGenericType
        private static readonly StringBuilder Builder = new StringBuilder();

        protected Unique() {
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