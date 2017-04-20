using GeneratorAPI;
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
        public static Generator<Provider> SharpyGenerator(this GeneratorFactory factory, Provider provider) {
            return factory.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         INameProvider contains methods which returns strings representing names.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Generator<INameProvider> NameGenerator(this GeneratorFactory factory, INameProvider provider) {
            return factory.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         ILongProvider contains methods which returns longs.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Generator<ILongProvider> LongGenerator(this GeneratorFactory factory, ILongProvider provider) {
            return factory.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         IDoubleProvider contains methods which returns doubles.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Generator<IDoubleProvider> DoubleGenerator(this GeneratorFactory factory,
            IDoubleProvider provider) {
            return factory.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         IIntegerProvider contains methods which returns integers.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Generator<IIntegerProvider> IntegerGenerator(this GeneratorFactory factory,
            IIntegerProvider provider) {
            return factory.Create(provider);
        }
    }
}