namespace DataGenerator.Types.Name
{
    internal class FirstName : Name
    {
        static FirstName() {
            NameList = DataGenHelperClass.ReadFromFile("Name/firstNames.txt");
        }

        public override string ToString() => Data;
    }
}