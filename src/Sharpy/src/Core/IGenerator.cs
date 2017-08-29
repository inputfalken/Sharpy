namespace Sharpy.Core {
    /// <summary>
    ///     <para>Represent a generator which can generate any amount of elements by invoking method <see cref="Generate" />.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> : IGenerator {
        /// <summary>
        ///     <para>Generate next element.</para>
        /// </summary>
        /// <returns>
        ///     A generated element.
        /// </returns>
        new T Generate();
    }

    /// <summary>
    ///     <para>Represent a generator which can generate any amount of objects by invoking method <see cref="Generate" />.</para>
    /// </summary>
    public interface IGenerator {
        /// <summary>
        ///     <para>Generate next element.</para>
        /// </summary>
        /// <returns>
        ///     A generated object.
        /// </returns>
        object Generate();
    }
}