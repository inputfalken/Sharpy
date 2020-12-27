namespace RandomExtended
{
    /// <summary>
    /// Specifies <see cref="Include"/> or <see cref="Exclude"/> behaviour.
    /// <remarks>
    ///   The <see cref="Include"/> <see cref="Rule"/> will always have precedence over <see cref="Exclude"/>, see the example.
    /// </remarks>
    /// <example>
    ///     <code language="c#">
    ///         var random = new Random();
    ///         var includePrecedence1 = random.Int(1,2, Rule.Include, Rule.Exclude); // Can either be 1 or 2.
    ///         var includePrecedence2 = random.Int(1,2, Rule.Exclude, Rule.Include); // Can either be 1 or 2.
    ///     </code>
    /// </example>
    /// </summary>
    public enum Rule
    {
        /// <summary>
        /// Specifies <see cref="Exclude"/> behaviour.
        /// </summary>
        Exclude = 0,

        /// <summary>
        /// Specifies <see cref="Include"/> behaviour.
        /// </summary>
        Include = 1
    }
}