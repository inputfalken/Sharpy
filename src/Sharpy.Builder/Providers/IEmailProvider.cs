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
        /// <param name="names">
        ///     The names of the email address.
        /// </param>
        string Mail(params string[] names);

        /// <summary>
        ///     Provides a System.String representing an email address.
        /// </summary>
        string Mail();

        string Mail(string name);
        string Mail(string firstName, string secondName);
        string Mail(string firstName, string secondName, string thirdName);
    }
}