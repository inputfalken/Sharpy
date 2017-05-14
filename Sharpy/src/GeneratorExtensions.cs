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
        public static Generator<Provider> Provider(this GeneratorFactory factory, Provider provider) {
            return Generator.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         INameProvider contains methods which returns strings representing names.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Generator<INameProvider> Name(this GeneratorFactory factory, INameProvider provider) {
            return Generator.Create(provider);
        }

        /// <summary>
        ///     <para>
        ///         ILongProvider contains methods which returns longs.
        ///     </para>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Generator<ILongProvider> Long(this GeneratorFactory factory, ILongProvider provider) {
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
        public static Generator<IDoubleProvider> Double(this GeneratorFactory factory, IDoubleProvider provider) {
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
        public static Generator<IIntegerProvider> Integer(this GeneratorFactory factory, IIntegerProvider provider) {
            return Generator.Create(provider);
        }
    }
}