using System.Collections.Generic;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Provides elements from System.Collections.Generic.IReadonlyList&lt;out T&gt;
    /// </summary>
    public interface IElementProvider
    {
        /// <summary>
        ///     Provides an element from the <paramref name="list" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="list" />.
        /// </typeparam>
        T Element<T>(IReadOnlyList<T> list);
    }
}