namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Method providing System.String representing an email address.
    /// </summary>
    public interface IEmailProvider
    {
        string Mail(string[] names);

        string Mail();

        string Mail(string name);
        string Mail(string firstName, string secondName);
        string Mail(string firstName, string secondName, string thirdName);
        string Mail(string firstName, string secondName, string thirdName, string fourthName);
        string Mail(string firstName, string secondName, string thirdName, string fourthName, string fifthName);

        string Mail(
            string firstName,
            string secondName,
            string thirdName,
            string fourthName,
            string fifthName,
            params string[] names
        );
    }
}