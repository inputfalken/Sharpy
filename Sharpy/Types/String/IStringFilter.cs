namespace Sharpy.Types.String {
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStringFilter<out T> {
        /// <summary>
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        T DoesNotStartWith(string arg);

        /// <summary>
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        T DoesNotContain(string arg);

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        T StartsWith(params string[] args);

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        T Contains(params string[] args);

        /// <summary>
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        T ByLength(int length);
    }
}