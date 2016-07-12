using System.Collections.Generic;

namespace DataGenerator.Types {
    public interface IGenerator {
        T Generate<T>(List<T> list);
    }
}