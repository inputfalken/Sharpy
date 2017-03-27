namespace Sharpy
{
    /// <summary>
    ///     <para>
    ///         A custom delegate with generation purpose.
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate T Generator<out T>();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<T> {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T GetProvider();
    }

    public class Generator : IGenerator<Provider> {
        private readonly Provider _provider;

        public Generator(Provider provider) {
            _provider = provider;
        }
        public Provider GetProvider() {
            return _provider;
        }
    }

}