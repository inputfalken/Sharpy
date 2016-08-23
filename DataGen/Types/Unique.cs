using System.Collections.Generic;
using System.Text;

namespace DataGen.Types {
    internal abstract class Unique<T> {
        private HashSet<T> HashSet { get; }
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