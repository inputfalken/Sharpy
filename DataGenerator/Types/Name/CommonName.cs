using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class CommonName
    {
        public CommonName(List<string> female, List<string> male, List<string> lastName) {
            Female = female;
            Male = male;
            LastName = lastName;
        }

        public List<string> Female { get; }
        public List<string> Male { get; }
        public List<string> LastName { get; }
    }
}