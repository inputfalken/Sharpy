using System;
using System.Collections.Generic;

namespace DataGenerator.Types
{
    public interface IGenerator<T>
    {
        T Generate(List<T> list);
        Tuple<T, int> GenerateWithIndex(List<T> list);
    }
}