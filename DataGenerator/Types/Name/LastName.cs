using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class LastName : Name
    {
        private static readonly IReadOnlyList<string> LastNames = DataGenHelperClass.ReadFromFile("Name/lastnames.txt");

        public LastName() {
            Data = DataGenHelperClass.FetchRandomItem(LastNames);
        }
    }
}