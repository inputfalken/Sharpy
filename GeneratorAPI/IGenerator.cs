namespace GeneratorAPI {
    /// <summary>
    ///     <para>Represents a generic generator which can generate any ammount of elements by using method <see cref="Generate"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> : IGenerator {
        /// <summary>
        ///     <para>Generate next element.</para>
        /// </summary>
        new T Generate();
    }

    /// <summary>
    ///     <para>Represent a generator which can generate any ammount of objcets by using method <see cref="Generate"/>.</para>
    /// </summary>
    public interface IGenerator {
        /// <summary>
        ///     <para>Generate next element.</para>
        /// </summary>
        object Generate();
    }
}