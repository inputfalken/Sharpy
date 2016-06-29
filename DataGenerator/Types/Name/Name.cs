using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal abstract class Name
    {
        protected static IReadOnlyList<string> NameList { private get; set; }
        public string Data { get; private set; }

        protected Name() {
            Data = DataGenHelperClass.FetchRandomItem(NameList);
        }
    }
}