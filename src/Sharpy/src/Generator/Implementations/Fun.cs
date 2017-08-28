using System;

namespace Sharpy.Generator.Implementations {
    /// <summary>
    ///     <para>A Generator using <see cref="Func{TResult}" /></para>
    /// </summary>
    internal sealed class Fun<T> : IGenerator<T> {
        private readonly Func<T> _fn;

        /// <summary>
        ///     <para>Creates a Generator&lt;T&gt; where each generation will invoke the argument.</para>
        ///     <remarks>
        ///         <para>
        ///             Do not instantiate types here.
        ///             If you want to instantiate types use  static method Generator.<see cref="Generator.Create{T}" />
        ///         </para>
        ///     </remarks>
        /// </summary>
        public Fun(Func<T> fn) {
            if (fn != null) _fn = fn;
            else throw new ArgumentNullException(nameof(fn));
        }


        /// <summary>
        ///     <para>Gives &lt;T&gt;</para>
        /// </summary>
        public T Generate() {
            return _fn();
        }

        object IGenerator.Generate() {
            return Generate();
        }
    }
}