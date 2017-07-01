namespace GeneratorAPI {
    /// <summary>
    ///     Represents a generic generator which can generate any ammount of elements by using method <see cref="Generate"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> : IGenerator {
        /// <summary>
        ///     Generate next element.
        /// </summary>
        new T Generate();
    }

    /// <summary>
    ///     Represent a generator which can generate any ammount of objcets by using method <see cref="Generate"/>.
    /// </summary>
    public interface IGenerator {
        /// <summary>
        ///     Generate next element.
        /// </summary>
        object Generate();
    }
}