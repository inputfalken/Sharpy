namespace GeneratorAPI {
    /// <summary>
    ///     Represents a result of a Generator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGeneration<out T> {
        /// <summary>
        ///     Take one element from the generation.
        /// </summary>
        /// <returns></returns>
        T Take();
    }
}