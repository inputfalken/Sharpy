using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    //TODO Make factory non static and use generator as a constructor and everythign will return Data<T>
    internal static class Factory
    {
        public static StringGenerator StringGenerator(IGenerator<string> iGenerator) {
            return new StringGenerator(iGenerator);
        }
    }

    internal class StringGenerator
    {
        private readonly IGenerator<string> _dataGenerator;

        public StringGenerator(IGenerator<string> dataGenerator) {
            _dataGenerator = dataGenerator;
        }

        public Data<string> FirstName(Gender gender)
            => new FirstName(gender, _dataGenerator);

        public Data<string> Country() => new Country(_dataGenerator);
    }
}