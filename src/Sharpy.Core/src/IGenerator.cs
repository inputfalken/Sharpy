namespace Sharpy.Core {
    /// <summary>
    ///     <para>Represent a generator which can generate any amount of elements by invoking method <see cref="Generate" />.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> {
        /// <summary>
        ///     <para>Generate next element.</para>
        /// </summary>
        /// <returns>
        ///     A generated element.
        /// </returns>  T Generate();
    }
}