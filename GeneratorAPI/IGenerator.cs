using System;

namespace GeneratorAPI {
    /// <summary>
    ///     <para>
    ///         Represents something which can produce
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> {
        /// <summary>
        ///     <para>
        ///         Produces T
        ///     </para>
        /// </summary>
        /// <returns></returns>
        T GetProvider();

        /// <summary>
        ///     Returns the Generation of the Func.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        IGeneration<TResult> Generate<TResult>(Func<T, TResult> fn);
    }
}