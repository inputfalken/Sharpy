using GeneratorAPI;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public static class GeneratorExtensions {
        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="Sharpy.Provider"/>.</para>
        ///     <para>Invoking <see cref="Generator.Select{TSource,TResult}(IGenerator{TSource},System.Func{TSource,TResult})"/></para>
        ///     <para>Gives you various options on what to <see cref="IGenerator{T}.Generate"/>.</para>
        /// </summary>
        public static IGenerator<Provider> Provider(this GeneratorFactory factory, Provider provider) {
            return Generator.Create(provider);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="string"/>.</para>
        ///     <para>Each invokation of <see cref="IGenerator{T}.Generate"/> will return a <see cref="string"/> representing a first name.</para>
        ///     <remarks>
        ///         <para>If an implementation of <see cref="INameProvider"/> is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/></para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(this GeneratorFactory factory, INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(provider.FirstName);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="string"/>.</para>
        ///     <para>Each invokation of <see cref="IGenerator{T}.Generate"/> will return a <see cref="string"/> representing a first name based on argument <see cref="Gender"/>.</para>
        ///     <remarks>
        ///         <para>If an implementation of <see cref="INameProvider"/> is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/></para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(this GeneratorFactory factory, Gender gender,
            INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(() => provider.FirstName(gender));
        }
    }
}