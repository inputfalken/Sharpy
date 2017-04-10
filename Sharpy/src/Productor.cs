﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Represents something which can produce
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProductor<out T> {
        /// <summary>
        ///     <para>
        ///         Produces T
        ///     </para>
        /// </summary>
        /// <returns></returns>
        T Produce();
    }

    /// <summary>
    /// </summary>
    public static class Productor {
        /// <summary>
        ///     <para>
        ///         Every time produce gets called this instance will be used.
        ///     </para>
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IProductor<T> Yield<T>(T item) {
            return new Identity<T>(item);
        }

        /// <summary>
        ///     Works like Yield except it will be deffered until produce gets called.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IProductor<T> Deferred<T>(Func<T> fn) {
            return new Deferred<T>(fn);
        }

        /// <summary>
        ///     <para>
        ///         Every time Produce gets invoked this function will be invoked.
        ///     </para>
        /// </summary>
        /// <param name="fn"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IProductor<T> Generate<T>(Func<T> fn) {
            return new Function<T>(fn);
        }

        /// <summary>
        ///     <para>
        ///         Turn an IEnumerable into IProductor.
        ///     </para>
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        public static IProductor<T> Sequence<T>(IEnumerable<T> enumerable) {
            return new Sequence<T>(enumerable.CacheGeneratedResults());
        }

        /// <summary>
        ///     <remarks>
        ///         Generates the result of the functions for every invokation of produce.
        ///     </remarks>
        /// </summary>
        /// <param name="productor"></param>
        /// <param name="fn"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IProductor<TResult> Generate<TSource, TResult>(this IProductor<TSource> productor,
            Func<TSource, TResult> fn) {
            return Generate(() => fn(productor.Produce()));
        }

        /// <summary>
        ///     <para>
        ///         Maps TSource to Tresult for one invokation of produce.
        ///     </para>
        ///     <remarks>
        ///         If you call this after any Generate method all generation results will be the same.
        ///         This should be used if you only want to Map once. for example System.Random.
        ///     </remarks>
        /// </summary>
        public static IProductor<TResult> Select<TSource, TResult>(this IProductor<TSource> productor,
            Func<TSource, TResult> fn) {
            return Deferred(() => fn(productor.Produce()));
        }


        /// <summary>
        ///     <para>
        ///         Zips two IProductors together and generates the result of the function for every invokation of produce.
        ///     </para>
        /// </summary>
        /// <param name="productor"></param>
        /// <param name="secondProductor"></param>
        /// <param name="func"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public static IProductor<TResult> GenerateZip<TResult, TSource, TSecond>(this IProductor<TSource> productor,
            IProductor<TSecond> secondProductor, Func<TSource, TSecond, TResult> func) {
            return Generate(() => func(productor.Produce(), secondProductor.Produce()));
        }

        /// <summary>
        ///     <para>
        ///         Zips two IProductors together for one invokation of produce.
        ///     </para>
        ///     <remarks>
        ///         If you call this after any Generate method all generation results will be the same.
        ///         This should be used if you only want to Map once. for example System.Random.
        ///     </remarks>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <param name="productor"></param>
        /// <param name="secondProductor"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IProductor<TResult> Zip<TResult, TSource, TSecond>(this IProductor<TSource> productor,
            IProductor<TSecond> secondProductor, Func<TSource, TSecond, TResult> func) {
            return Deferred(() => func(productor.Produce(), secondProductor.Produce()));
        }

        /// <summary>
        ///     <para>
        ///         Zips a IProductor together with a IEnumerable and generates the result of the function for every invokation of
        ///         produce.
        ///     </para>
        /// </summary>
        /// <param name="productor"></param>
        /// <param name="secondEnumerable"></param>
        /// <param name="func"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public static IProductor<TResult> GenerateZip<TResult, TSource, TSecond>(this IProductor<TSource> productor,
            IEnumerable<TSecond> secondEnumerable, Func<TSource, TSecond, TResult> func) {
            var enumerable = productor as IEnumerable<TSource>;
            return enumerable != null
                ? Sequence(enumerable.Zip(secondEnumerable, func))
                : Sequence(secondEnumerable.Select(second => func(productor.Produce(), second)));
        }

        /// <summary>
        ///     <para>
        ///         Returns an IEenumerable as long as count.
        ///     </para>
        /// </summary>
        /// <param name="productor"></param>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        public static IEnumerable<TSource> Take<TSource>(this IProductor<TSource> productor, int count) {
            for (var i = 0; i < count; i++) yield return productor.Produce();
        }

        /// <summary>
        ///     <para>
        ///         Creates an array as long as count
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="productor"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static TSource[] ToArray<TSource>(this IProductor<TSource> productor, int count) {
            return productor.Take(count).ToArray();
        }

        /// <summary>
        ///     <para>
        ///         Creates a List as long as count
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="productor"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<TSource> ToList<TSource>(this IProductor<TSource> productor, int count) {
            return productor.Take(count).ToList();
        }

        /// <summary>
        ///     <para>
        ///         Returns an never ending enumerable.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="productor"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> TakeForever<TSource>(this IProductor<TSource> productor) {
            while (true) yield return productor.Produce();
        }
    }
}