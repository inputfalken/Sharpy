namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Method providing System.String that represents an email address.
    /// </summary>
    public interface IEmailProvider
    {
        /// <summary>
        ///  Provides a System.String that represents an email address built from <paramref name="names"/>.
        /// </summary>
        /// <param name="names">
        ///  The System.String[] to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(in string[] names);

        /// <summary>
        ///  Provides a System.String that represents an email address.
        /// </summary>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail();

        /// <summary>
        ///  Provides a System.String that represents an email address built from <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        ///  The System.String to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(in string name);

        /// <summary>
        ///  Provides a System.String that represents an email address built from the arguments.
        /// </summary>
        /// <param name="firstName">
        ///  The first System.String to build from.
        /// </param>
        /// <param name="secondName">
        ///  The secondary System.String to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(in string firstName, in string secondName);

        /// <summary>
        ///  Provides a System.String that represents an email address built from the arguments.
        /// </summary>
        /// <param name="firstName">
        ///  The first System.String to build from.
        /// </param>
        /// <param name="secondName">
        ///  The secondary System.String to build from.
        /// </param>
        /// <param name="thirdName">
        ///  The third System.String to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(in string firstName, in string secondName, in string thirdName);

        /// <summary>
        ///  Provides a System.String that represents an email address built from the arguments.
        /// </summary>
        /// <param name="firstName">
        ///  The first System.String to build from.
        /// </param>
        /// <param name="secondName">
        ///  The secondary System.String to build from.
        /// </param>
        /// <param name="thirdName">
        ///  The third System.String to build from.
        /// </param>
        /// <param name="fourthName">
        ///  The fourth System.String to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName);

        /// <summary>
        ///  Provides a System.String that represents an email address built from the arguments.
        /// </summary>
        /// <param name="firstName">
        ///  The first System.String to build from.
        /// </param>
        /// <param name="secondName">
        ///  The secondary System.String to build from.
        /// </param>
        /// <param name="thirdName">
        ///  The third System.String to build from.
        /// </param>
        /// <param name="fourthName">
        ///  The fourth System.String to build from.
        /// </param>
        /// <param name="fifthName">
        ///  The fifth System.String to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName, in string fifthName);

        /// <summary>
        ///  Provides a System.String that represents an email address built from the arguments.
        /// </summary>
        /// <param name="firstName">
        ///  The first System.String to build from.
        /// </param>
        /// <param name="secondName">
        ///  The secondary System.String to build from.
        /// </param>
        /// <param name="thirdName">
        ///  The third System.String to build from.
        /// </param>
        /// <param name="fourthName">
        ///  The fourth System.String to build from.
        /// </param>
        /// <param name="fifthName">
        ///  The fifth System.String to build from.
        /// </param>
        /// <param name="additional">
        ///  The <paramref name="additional"/> to build from.
        /// </param>
        /// <returns>
        ///  A formatted email address.
        /// </returns>
        string Mail(
            in string firstName,
            in string secondName,
            in string thirdName,
            in string fourthName,
            in string fifthName,
            params string[] additional
        );
    }
}