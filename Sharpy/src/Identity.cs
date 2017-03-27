namespace Sharpy {
    internal class Identity<T> : IProductor<T> {
        private readonly T _t;

        public Identity(T t) {
            _t = t;
        }

        public T Produce() {
            return _t;
        }
    }
}