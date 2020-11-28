using System;
using static System.Reactive.Linq.Observable;

namespace Sharpy.ReactiveBuilder {
    internal class ReactiveBuilder<TBuilder> : IObservable<TBuilder> where TBuilder : Builder.Builder {
        private readonly IObservable<TBuilder> _observable;

        /// <summary>
        ///     Creates a <see cref="IObservable{T}" /> with <see cref="Builder" /> as its generic type.
        /// </summary>
        /// <param name="builder">
        ///     A <see cref="Builder" /> or one of its descenders.
        /// </param>
        public ReactiveBuilder(TBuilder builder) {
            _observable = Generate(
                builder,
                b => true,
                b => b,
                b => b
            );
        }

        /// <inheritdoc cref="IObservable{T}.Subscribe" />
        public IDisposable Subscribe(IObserver<TBuilder> observer)
        {
            return _observable.Subscribe(observer);
        }
    }

    public static class ReactiveBuilder {
        /// <summary>
        ///     Creates an <see cref="IObservable{T}" /> from a <see cref="Builder" /> or one if its descenders.
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of <see cref="TBuilder" /> or one if its descenders.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the result returned by the <paramref name="selector" />.
        /// </typeparam>
        /// <param name="builder">
        ///     The <see cref="TBuilder" /> who the <paramref name="selector" /> have as its input.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each source element.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}" /> whose generic type is the result of the <paramref name="selector" />.
        /// </returns>
        public static IObservable<TResult> Observable<TBuilder, TResult>(this TBuilder builder,
            Func<TBuilder, TResult> selector)
            where TBuilder : Builder.Builder
        {
            return new ReactiveBuilder<TBuilder>(builder ?? throw new ArgumentNullException(nameof(builder)))
                .Select(selector ?? throw new ArgumentNullException(nameof(selector)));
        }

        /// <summary>
        ///     Creates an <see cref="IObservable{T}" /> from a <see cref="Builder" /> or one if its descenders.
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of <see cref="TBuilder" /> or one if its descenders.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the result returned by the <paramref name="selector" />.
        /// </typeparam>
        /// <param name="builder">
        ///     The <see cref="TBuilder" /> who the <paramref name="selector" /> have as its input.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each source element; the second parameter of the function represents a counter for
        ///     each element built.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}" /> whose generic type is the result of the <paramref name="selector" />.
        /// </returns>
        public static IObservable<TResult> Observable<TBuilder, TResult>(this TBuilder builder,
            Func<TBuilder, int, TResult> selector)
            where TBuilder : Builder.Builder
        {
            return new ReactiveBuilder<TBuilder>(builder ?? throw new ArgumentNullException(nameof(builder)))
                .Select(selector ?? throw new ArgumentNullException(nameof(selector)));
        }
    }
}