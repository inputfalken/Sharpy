using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Factory
    {
        //public static Data<string> GetLastName() => new LastName(new RandomGenerator());

        public static Data<string> GetFirstName(Gender gender, IGenerator<string> iGenerator)
            => new FirstName(gender, iGenerator);

        public static Country GetCountry(IGenerator<string> iGenerator) {
            return new Country(iGenerator);
        }
    }
}