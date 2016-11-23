namespace Sharpy.IProviders {
    /// <summary>
    /// <para>Represents methods for giving integers.</para>
    /// </summary>
    public interface IIntegerProvider {
        /// <summary>
        ///     <para>Returns a Integer from 0 to max.</para>
        ///     <param name="max">The max value</param>
        /// </summary>
        int Integer(int max);

        /// <summary>
        ///     <para>Returns a Integer from min to max.</para>
        /// </summary>
        int Integer(int min, int max);

        /// <summary>
        ///     <para>Returns a Integer within all possible values of integer (except int.MaxValue)</para>
        /// </summary>
        /// <returns></returns>
        int Integer();
    }
}