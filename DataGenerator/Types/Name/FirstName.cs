using System;
using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class FirstName : Data<string>
    {
        private static readonly List<string> Male =
            ReadFromFile("Data/Types/Name/maleNames.txt");

        private static readonly List<string> Female =
            ReadFromFile("Data/Types/Name/femaleNames.txt");

        public string Name { get; }

        public FirstName(Gender gender, IFetchable<string> iFetchable) : base(iFetchable) {
            switch (gender) {
                case Gender.Female:
                    Name = IfFetchable.Fetch(Female);
                    break;
                case Gender.Male:
                    Name = IfFetchable.Fetch(Male);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }
    }
}