namespace Sharpy.Builder.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="string" /> representing user name.
    ///     </para>
    /// </summary>
    public interface IUserNameProvider {
        /// <summary>
        ///     Creates a string representing a user name.
        /// </summary>
        /// <returns>
        ///     A string representing a user name.
        /// </returns>
        string UserName();
    }
}