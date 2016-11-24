using Sharpy.Enums;

namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Represents a method for providing Names.</para>
    /// </summary>
    public interface INameProvider {
        /// <summary>
        ///     Returns a name depending on the arguments.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        string Name(NameType arg);
    }

    /// <summary>
    ///     Represents a parameterless method providing strings.
    /// </summary>
    public interface IStringProvider {
        /// <summary>
        ///     Returns a string.
        /// </summary>
        /// <returns></returns>
        string String();
    }
}