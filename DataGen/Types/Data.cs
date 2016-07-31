using System;
using System.Collections.Generic;

namespace DataGen.Types {
    public class Data<TData, TFilter> where TFilter : Filter<TData> {
        private IEnumerable<TData> Datas { get; }
        private TFilter Filter { get; }

        private Func<IEnumerable<TData>, TFilter> Factory { get; }


        public Data(IEnumerable<TData> data, Func<IEnumerable<TData>, TFilter> factory) {
            Datas = data;
            Factory = factory;
        }

        public Data(IEnumerable<TData> data, TFilter filter) {
            Datas = data;
            Filter = filter;
        }


        public IEnumerable<TData> Collection(Func<TFilter, IEnumerable<TData>> func)
            => func(Filter);
    }
}