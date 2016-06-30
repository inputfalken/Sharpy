using System;
using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class FirstName : Name
    {
        private const string Directory = "Name";

        public FirstName(Gender gender) {
            switch (gender) {
                case Gender.Female:
                    Female = Female ?? DataGenHelperClass.ReadFromFile($"{Directory}/femaleNames.txt");
                    Data = DataGenHelperClass.FetchRandomItem(Female);
                    break;
                case Gender.Male:
                    Male = Male ?? DataGenHelperClass.ReadFromFile($"{Directory}/maleNames.txt");
                    Data = DataGenHelperClass.FetchRandomItem(Male);
                    break;
                case Gender.Mixed:
                    Mixed = Mixed ?? DataGenHelperClass.ReadFromFile($"{Directory}/firstNames.txt");
                    Data = DataGenHelperClass.FetchRandomItem(Mixed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }

        private static IReadOnlyList<string> Male { get; set; }
        private static IReadOnlyList<string> Female { get; set; }
        private static IReadOnlyList<string> Mixed { get; set; }
    }
}