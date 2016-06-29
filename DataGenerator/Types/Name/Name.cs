using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal abstract class Name
    {
        protected static IReadOnlyList<string> NameList { private get; set; }
        public string Data { get; private set; }

        //TODO Implement a enum to decide what type of name you want Example Female/male
        protected Name() {
            Data = DataGenHelperClass.FetchRandomItem(NameList);
        }
    }
}