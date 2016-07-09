using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFactory = new NameFunctionFactory(new RandomGenerator());
            var generateName = nameFactory.GenerateName();
        }
    }
}