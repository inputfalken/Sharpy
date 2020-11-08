namespace Sharpy.Builder.IProviders {
    /// <summary>
    ///     <para>
    ///         Method providing System.String representing an email address.
    ///     </para>
    /// </summary>
    public interface IEmailProvider {
        /// <summary>
        ///     Provides a System.String representing an email address.
        /// </summary>
        /// <param name="name">
        ///     The names of the email address.
        /// </param>
        string Mail(params string[] name);

        /// <summary>
        ///     Provides a System.String representing an email address.
        /// </summary>
        string Mail();
    }
}