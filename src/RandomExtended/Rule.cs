namespace RandomExtended
{
    /// <summary>
    /// Specifies the rule min and max based arguments.
    /// <remarks>
    ///   The inclusive rules will always have precedence over exclusive, see the example.
    /// </remarks>
    /// <example>
    ///     <code language="c#">
    ///         var random = new Random();
    ///         var includePrecedence1 = random.Int(1,2, Rule.InclusiveExclusive); // Can either be 1 or 2.
    ///         var includePrecedence2 = random.Int(1,2, Rule.ExclusiveInclusive); // Can either be 1 or 2.
    ///     </code>
    /// </example>
    /// </summary>
    public enum Rule
    {
        /// <summary>
        ///  Inclusive min and max arg.
        /// </summary>
        Inclusive,

        /// <summary>
        /// Exclusive min and max arg.
        /// </summary>
        Exclusive,

        /// <summary>
        /// Inclusive min arg and exclusive max arg.
        /// </summary>
        InclusiveExclusive,

        /// <summary>
        /// Exclusive min arg and exclusive max arg.
        /// </summary>
        ExclusiveInclusive
    }
}