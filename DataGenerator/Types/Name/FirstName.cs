using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGenerator.Types.Name
{
    internal class FirstName : Name
    {
        private const string Directory = "Name";

        private static readonly IReadOnlyList<string> Male =
            DataGenHelperClass.ReadFromFile($"{Directory}/maleNames.txt");

        private static readonly IReadOnlyList<string> Female =
            DataGenHelperClass.ReadFromFile($"{Directory}/femaleNames.txt");


        public FirstName(Gender gender) {
            switch (gender) {
                case Gender.Female:
                    Data = DataGenHelperClass.FetchRandomItem(Female);
                    break;
                case Gender.Male:
                    Data = DataGenHelperClass.FetchRandomItem(Male);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }
    }
}