﻿using System.Collections.Generic;
using System.IO;
using DataGenerator.Types.Name;

namespace DataGenerator.Types
{
    public class Country : Data<string>
    {
        private static readonly List<string> Titles = ReadFromFile("Data/Types/Country/country.txt");

        private static readonly IReadOnlyList<string> SeoCodes =
            ReadFromFile("Data/Types/Country/seoCode.txt");


        public Country(IFetchable<string> ifFetchable) : base(ifFetchable) {
            var fetchWithIndex = IfFetchable.FetchWithIndex(Titles);
            Title = fetchWithIndex.Item1;
            IsoCode = SeoCodes[fetchWithIndex.Item2];
        }

        private string Title { get; }
        private string IsoCode { get; }
    }
}