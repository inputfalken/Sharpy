using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal class DataFactory
    {
        //Create a method where the user can use the _datagenerator on a provided list
        private readonly IGenerator<string> _dataGenerator;

        public DataFactory(IGenerator<string> dataGenerator) {
            _dataGenerator = dataGenerator;
        }


        public Data FirstName(Gender gender)
            => new FirstName(gender, _dataGenerator);

        //public Data Country() => new Country(_dataGenerator);
    }
}