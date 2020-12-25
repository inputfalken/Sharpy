namespace RandomExtended
{
    /// <summary>
    /// Specifies an inclusive or exclusive operation to be used for <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    public readonly struct Clusivity<T>
    {
        /// <summary>
        /// Gets the <see cref="Value"/>.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the <see cref="Rule"/>
        /// </summary>
        public Rule Rule { get; }

        /// <summary>
        /// Creates a <see cref="Clusivity{T}"/>.
        /// </summary>
        /// <param name="value">
        /// Sets <see cref="Value"/>.
        /// </param>
        /// <param name="rule">
        /// Sets <see cref="Rule"/>.
        /// </param>
        public Clusivity(in T value, in Rule rule)
        {
            Value = value;
            Rule = rule;
        }
    }

    /// <summary>
    /// Specifies an <see cref="Inclusive"/> or <see cref="Exclusive"/> behaviour.
    /// </summary>
    public enum Rule
    {
        /// <summary>
        /// Specifies an <see cref="Exclusive"/> behaviour.
        /// </summary>
        Exclusive = 0,

        /// <summary>
        /// Specifies an <see cref="Inclusive"/> behaviour.
        /// </summary>
        Inclusive = 1
    }
}