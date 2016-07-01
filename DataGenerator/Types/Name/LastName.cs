using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class LastName : Data<string>
    {
        private static readonly List<string> LastNames = ReadFromFile("Name/lastnames.txt");
        public string Name { get; }

        public LastName(IFetchable<string> iFetchable) : base(iFetchable) {
            Name = IfFetchable.Fetch(LastNames);
        }
    }
}