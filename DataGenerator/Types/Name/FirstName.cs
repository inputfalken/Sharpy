using System;
using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class FirstName : Data
    {
        private static readonly List<string> Male =
            ReadFromFile("Data/Types/Name/maleNames.txt");

        private static readonly List<string> Female =
            ReadFromFile("Data/Types/Name/femaleNames.txt");

        public string Name { get; }

        public FirstName(Gender gender, IGenerator<string> iGenerator) : base(iGenerator) {
            switch (gender) {
                case Gender.Female:
                    Name = Generator.Generate(Female);
                    break;
                case Gender.Male:
                    Name = Generator.Generate(Male);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }
    }
}