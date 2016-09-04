using DataGen;
using DataGen.Types;
using DataGen.Types.Date;
using DataGen.Types.Name;
using NodaTime;

namespace DataGen {
    public class Randomizer {
        internal readonly Config Config;

        public Randomizer(Config config) {
            Config = config;
        }

        ///<summary>
        ///     Gives a random name, it could be a female first name, male first name and a lastname.
        /// </summary>
        public string Name() => Config.NameFilter.RandomItem.Data;

        ///<summary>
        ///     Gives a random name based on type of argument.
        /// </summary>
        public string NameByType(NameTypes nameTypes) => Config.NameFilter.ByType(nameTypes).RandomItem.Data;

        ///<summary>
        ///     Gives a random username from a huge collection.
        /// </summary>
        public string UserName() => Config.Usernames.RandomItem;

        ///<summary>
        ///     Gives a random bool
        /// </summary>
        public bool Bool() => Number(2) != 0;

        ///<summary>
        ///     Gives a random number within below the argument value
        /// </summary>
        public int Number(int maxNum) => HelperClass.SetRandomizer(maxNum);

        ///<summary>
        ///     Gives a random number within within the two arguments
        /// </summary>
        public int Number(int minNum, int maxNum) => HelperClass.SetRandomizer(minNum, maxNum);

        ///<summary>
        ///     gives a date with random month & date and subtract the current the current year by the argument
        /// </summary>
        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        ///<summary>
        ///     Gives a random month & date and use the argument given as year
        /// </summary>
        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        ///<summary>
        ///     gives a random phonenumber using a random country code and lets you specify a number to start with as well as the length.
        /// </summary>
        public string PhoneNumber(string preNumber = null, int length = 4) =>
            Config.PhoneNumberGenerator.RandomNumber(length, preNumber);

        ///<summary>
        ///     Gives a mail address by concatining the arguments into a mail address.
        /// </summary>
        public string MailAdress(string name, string secondName = null) => Config.MailGenerator.Mail(name, secondName);
    }
}