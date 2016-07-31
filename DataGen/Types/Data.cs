using System;
using System.Collections.Generic;

namespace DataGen.Types {
    public abstract class Data<TData, TFilter> where TFilter : Filter<TData> {
        protected abstract IEnumerable<TData> Datas { get; }

        private Func<IEnumerable<TData>, TFilter> Factory { get; }

        protected Data(Func<IEnumerable<TData>, TFilter> factory) {
            Factory = factory;
        }


        public IEnumerable<TData> Collection(Func<TFilter, IEnumerable<TData>> func)
            => func(Factory(Datas));
    }
}