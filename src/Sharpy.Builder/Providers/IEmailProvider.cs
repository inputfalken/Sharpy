namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Method providing System.String representing an email address.
    /// </summary>
    public interface IEmailProvider
    {
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