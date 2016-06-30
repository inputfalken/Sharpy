namespace DataGenerator.Types.Name
{
    //TODO FIND A WAY TO GET GENDER SEPERATED NAMES 
    internal class FirstName : Name
    {
        static FirstName() {
            NameList = DataGenHelperClass.ReadFromFile("Name/firstNames.txt");
        }
    }
}