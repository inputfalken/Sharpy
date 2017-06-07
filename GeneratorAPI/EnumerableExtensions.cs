using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        ///     Flattens a IGenerator with a IEnumerable
        /// </summary>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, IEnumerable<TResult>> enumerableSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (enumerableSelector == null) throw new ArgumentNullException(nameof(enumerableSelector));
            var sequence = new Seq<TResult>(() => enumerableSelector(generator.Generate()));
            return Generator.Function(() => sequence.Generate());
        }

        /// <summary>
        ///     Flattens a IGenerator with a IEnumerable And composes the values
        /// </summary>
        public static IGenerator<TCompose> SelectMany<TSource, TResult, TCompose>(this IGenerator<TSource> generator,
            Func<TSource, IEnumerable<TResult>> enumerableSelector, Func<TSource, TResult, TCompose> composer) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (enumerableSelector == null) throw new ArgumentNullException(nameof(enumerableSelector));
            if (composer == null) throw new ArgumentNullException(nameof(composer));
            return generator.SelectMany(source => enumerableSelector(source)
                .Select(result => composer(source, result)));
        }
    }
}
