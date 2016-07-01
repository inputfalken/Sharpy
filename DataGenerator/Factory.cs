using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Factory
    {
        public static Name GetLastName() => new LastName(new RandomFetcher());

        public static Name GetFirstName(Gender gender) => new FirstName(gender, new RandomFetcher());

        public static Country GetCountry() {
            return new Country(new RandomFetcher());
        }
    }
}