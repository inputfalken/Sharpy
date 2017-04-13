using System;

namespace GeneratorAPI {
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateGeneration<T> : IGeneration<T> {
        private readonly Func<T> _fn;

        /// <summary>
        /// </summary>
        /// <param name="fn"></param>
        public DelegateGeneration(Func<T> fn) {
            _fn = fn;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public T Take() {
            return _fn();
        }
    }
}