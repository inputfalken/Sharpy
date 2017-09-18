namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Method providing <see cref="string" /> representing an email address.
    ///     </para>
    /// </summary>
    public interface IEmailProvider {
        /// <summary>
        ///     Creates a string representing an email address.
        /// </summary>
        /// <param name="name">
        ///     The names of the email address.
        /// </param>
        /// <returns>
        ///     A string representing an email address.
        /// </returns>
        string Mail(params string[] name);
    }
}