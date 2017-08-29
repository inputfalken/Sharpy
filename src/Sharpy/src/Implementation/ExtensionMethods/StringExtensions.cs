using System.Text;

namespace Sharpy.Implementation.ExtensionMethods {
    internal static class StringExtensions {
        private static readonly StringBuilder Builder = new StringBuilder();

        public static string Append(this string str, params object[] elements) {
            Builder.Clear();
            Builder.Append(str);
            foreach (var element in elements) Builder.Append(element);
            return Builder.ToString();
        }

        internal static string Prefix<T>(this T item, int ammount) => new string('0', ammount).Append(item);
    }
}