using System.Collections.Generic;

namespace Sharpy.IProviders {
    public interface IListElementPicker {
        T TakeElement<T>(IReadOnlyList<T> list);

        T TakeArgument<T>(params T[] list);
    }
}