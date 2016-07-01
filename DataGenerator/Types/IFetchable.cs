using System;
using System.Collections.Generic;

namespace DataGenerator.Types
{
    public interface IFetchable<T>
    {
        //Maybe add read file here? 
        T Fetch(List<T> list);
        Tuple<T, int> FetchWithIndex(List<T> list);
    }
}