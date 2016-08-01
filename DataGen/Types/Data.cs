using System;
using System.Collections.Generic;

namespace DataGen.Types {
    // IS this class even needed?
    public class Data<TData, TFilter> where TFilter : Filter<TData> {
        private TFilter Filter { get; }


        
        public Data(TFilter filter) {
            Filter = filter;
        }


        public IEnumerable<TData> Collection(Func<TFilter, IEnumerable<TData>> func)
            => func(Filter);
    }
}