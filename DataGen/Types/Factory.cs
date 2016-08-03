using System;
using System.Collections.Generic;
using System.IO;
using DataGen.Types.Name;
using Newtonsoft.Json;

namespace DataGen.Types {
    public static class Factory {
        //Todo move this prop to name class

        public static TFilter Filter<TFilter, TData>(Func<IEnumerable<TData>, TFilter> func,
            IEnumerable<TData> collection) where TFilter : Filter<TData>
            => func(collection);
    }
}