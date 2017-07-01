using GeneratorAPI;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public static class GeneratorExtensions {
        /// <summary>
        ///     <para>
        ///         <see cref="Sharpy.Provider"/> contains various methods for generating common data types.
        ///         To get the same result every time you execute the program use the seed overload constructor.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IGenerator<Provider> Provider(this GeneratorFactory factory, Provider provider) {
            return Generator.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         Returns a generator which randomizes first names.
        ///     </para>
        ///     <remarks>
        ///         If a implementation of <see cref="INameProvider"/> is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(this GeneratorFactory factory, INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(provider.FirstName);
        }

        /// <summary>
        ///     <para>
        ///         Returns a generator which randomizes first names based on gender supplied.
        ///     </para>
        ///     <remarks>
        ///         If a implementation of <see cref="INameProvider"/> is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(this GeneratorFactory factory, Gender gender,
            INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(() => provider.FirstName(gender));
        }
    }
}