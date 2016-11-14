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
    }
}