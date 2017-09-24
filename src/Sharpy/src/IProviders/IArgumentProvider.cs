namespace Sharpy.IProviders {
    /// <summary>
    ///     Returns elements from arguments.
    /// </summary>
    public interface IArgumentProvider {
        /// <summary>
        ///     Returns one of the arguments.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the arguments.
        /// </typeparam>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <param name="additional">
        ///     The additional arguments.
        /// </param>
        /// <returns>
        ///     One of the elements from the arguments.
        /// </returns>
        T Argument<T>(T first, T second, params T[] additional);
    }
}