using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class LastName : Name
    {
        private static readonly List<string> LastNames = ReadFromFile("Name/lastnames.txt");

        public LastName(IFetchable<string> iFetchable) : base(iFetchable) {
            Data = Fetchable.Fetch(LastNames);
        }
    }
}