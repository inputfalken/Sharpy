using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Factory
    {
        //public static Data<string> GetLastName() => new LastName(new RandomFetcher());

        public static Data<string> GetFirstName(Gender gender, IFetchable<string> iFetchable)
            => new FirstName(gender, iFetchable);

        public static Country GetCountry(IFetchable<string> iFetchable) {
            return new Country(iFetchable);
        }
    }
}