using System;
using System.Collections.Generic;

namespace DataGenerator.Types
{
    public class RandomFetcher : IFetchable<string>
    {
        public string Fetch(List<string> list)
            => list[HelperClass.Randomer(list.Count)];

        public Tuple<string, int> FetchWithIndex(List<string> list) {
            var currentIndex = HelperClass.Randomer(list.Count);
            return new Tuple<string, int>(list[currentIndex], currentIndex);
        }
    }
}