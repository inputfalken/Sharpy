using System;
using Sharpy.Core.Implementations;

namespace Sharpy.Core.Linq
{
    public static partial class Extensions
    {
        public static IGenerator<TResult> SelectMany<T, TResult>(
            this IGenerator<T> source,
            Func<T, IGenerator<TResult>> selector
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return new Fun<TResult>(() => selector(source.Generate()).Generate());
        }

        public static IGenerator<TResult> SelectMany<T, TSelect, TResult>(
            this IGenerator<T> source,
            Func<T, IGenerator<TSelect>> selector,
            Func<T, TSelect, TResult> resultSelector
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (resultSelector is null)
                throw new ArgumentNullException(nameof(resultSelector));

            return source.SelectMany(x => selector(x).Select(y => resultSelector(x, y)));
        }
    }
}