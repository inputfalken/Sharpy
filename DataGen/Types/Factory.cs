using DataGen.Types.Name;

namespace DataGen.Types {
    public static class Factory {
        public static FileBasedData<TData, TFactory> FileBasedData<TData, TFactory>(
            FileBasedData<TData, TFactory> dataTypeFactory)
            => dataTypeFactory;
    }
}