namespace DataGenerator.Types.Name.Regions
{
    internal interface IRegion
    {
        void SetCountries(NameData nameData);
        Country GetCountry();
    }
}