﻿using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace DataGen {
    public static class ExtensionMethods {
        /// <summary>
        ///     Will add elements to list in pattern by func
        /// </summary>
        /// <param name="list">the list to obtain the sequence</param>
        /// <param name="ammount">the ammount of elements</param>
        /// <param name="func">will return the element with created by arguments</param>
        /// <param name="defaultTsource">The start value of the sequence</param>
        /// <returns></returns>
        public static IEnumerable<TSource> CreatePattern<TSource>(this IList<TSource> list, int ammount,
            Func<TSource, int, TSource> func, TSource defaultTsource = default(TSource)) {
            foreach (var current in Range(1, ammount))
                list.Add(func(defaultTsource, current));
            return list;
        }

        /// <summary>
        ///     Will perform a foreach loop
        /// </summary>
        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> func) {
            foreach (var source in enumerable)
                func(source);
        }
    }
}