namespace DataGenerator.Types.Name
{
    internal class LastName : Name
    {
        static LastName() {
            NameList = DataGenHelperClass.ReadFromFile("Name/lastnames.txt");
        }

        public override string ToString() => Data;
    }
}