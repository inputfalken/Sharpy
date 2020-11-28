﻿using System.Text;

namespace Sharpy.Builder.Implementation.ExtensionMethods
{
    internal static class StringExtensions
    {
        private static readonly StringBuilder Builder = new StringBuilder();

        internal static string Append(this string str, params object[] elements)
        {
            Builder.Clear();
            Builder.Append(str);
            foreach (var element in elements) Builder.Append(element);
            return Builder.ToString();
        }

        internal static string Prefix<T>(this T item, int amount)
        {
            var str = new string('0', amount);
            return item is null ? str : str.Append(item);
        }
    }
}