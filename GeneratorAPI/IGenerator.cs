namespace GeneratorAPI {
    /// <summary>
    ///     Represents a generic generator which can generate any ammount of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<out T> : IGenerator {
        /// <summary>
        ///     Generate next element.
        /// </summary>
        T Generate();
    }

    public interface IGenerator {
        object Generate();
    }
}