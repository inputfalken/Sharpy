namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Represents methods for providing integers.</para>
    /// </summary>
    public interface IIntegerProvider {
        /// <summary>
        ///     <para>Returns a Integer from 0 to max.</para>
        ///     <param name="max">The maximum value.</param>
        /// </summary>
        int Integer(int max);

        /// <summary>
        ///     <para>Returns a integer within min and max value.</para>
        /// </summary>
        int Integer(int min, int max);

        /// <summary>
        ///     <para>Returns and integer within max and min value of int32.</para>
        /// </summary>
        /// <returns></returns>
        int Integer();
    }
}