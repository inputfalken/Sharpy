using System.Collections.Immutable;

namespace DataGenerator.Types {
    public interface IGenerator {
        T Generate<T>(ImmutableList<T> list);
    }
}