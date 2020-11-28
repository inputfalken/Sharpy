namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.String representing user name.
    /// </summary>
    public interface IUserNameProvider
    {
        /// <summary>
        ///     Provides a System.String representing a user name.
        /// </summary>
        string UserName();
    }
}