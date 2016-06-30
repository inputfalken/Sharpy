using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class LastName : Name
    {
        private IReadOnlyList<string> LastNames { get; }

        public LastName() {
            if (LastNames == null)
                LastNames = DataGenHelperClass.ReadFromFile("Name/lastnames.txt");
            Data = DataGenHelperClass.FetchRandomItem(LastNames);
        }
    }
}