using System.Security.Claims;
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
        ///         Provider Contains various methods for generating data.
        ///         To get the same result every time you execute the program use the seed overload constructor.
        ///         If want you to add your own methods you can derive from this class.
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
        ///     <para>
        ///         ILongProvider contains methods which returns longs.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IGenerator<ILongProvider> Long(this GeneratorFactory factory, ILongProvider provider) {
            return Generator.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         IDoubleProvider contains methods which returns doubles.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IGenerator<IDoubleProvider> Double(this GeneratorFactory factory, IDoubleProvider provider) {
            return Generator.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         IIntegerProvider contains methods which returns integers.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IGenerator<IIntegerProvider> Integer(this GeneratorFactory factory, IIntegerProvider provider) {
            return Generator.Create(provider);
        }
    }
}