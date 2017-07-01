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
        ///         Provider Contains various methods for generating common data types.
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
        ///         Returns a generator which randomizes First names.
        ///     </para>
        ///     <remarks>
        ///         If INameProvider is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(this GeneratorFactory factory, INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(provider.FirstName);
        }

        /// <summary>
        ///     <para>
        ///         Returns a generator which randomizes First names based on gender supplied.
        ///     </para>
        ///     <remarks>
        ///         If INameProvider is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(this GeneratorFactory factory, Gender gender,
            INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(() => provider.FirstName(gender));
        }

        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="string"/>.
        ///     Each invokation of <see cref="IGenerator{T}.Generate"/> will return a <see cref="string"/> representing a last name.
        ///     <para> </para>
        ///     <remarks>
        ///         If an implementation of <see cref="INameProvider"/> is not supplied the implementation will get defaulted to <see cref="NameByOrigin"/>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> LastName(this GeneratorFactory factory, INameProvider lastNameProvider = null) {
            lastNameProvider = lastNameProvider ?? new NameByOrigin();
            return Generator.Function(lastNameProvider.LastName);
        }
    }
}