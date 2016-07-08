using System.Collections.Generic;

namespace DataGenerator.Types.Name.Regions
{
    internal interface IRegion
    {
        void SetCountries(IEnumerable<Region> regions);
        Country GetCountry();
    }
}