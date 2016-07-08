using System.Collections.Generic;

namespace DataGenerator.Types
{
    public interface IGenerator<T>
    {
        T Generate(List<T> list);
    }
}