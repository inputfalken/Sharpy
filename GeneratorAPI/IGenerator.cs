namespace GeneratorAPI {
    /// <summary>
    ///     Represents a generic generator which can generate any ammount of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> : IGenerator {
        /// <summary>
        ///     Generate next element.
        /// </summary>
        new T Generate();
    }

    /// <summary>
    ///     Represent a generator which can generate any ammount of elements.
    /// </summary>
    public interface IGenerator {
        /// <summary>
        ///     Generate next element.
        /// </summary>
        object Generate();
    }
}