using DataGen.Types;
using DataGen.Types.CountryCode;
using DataGen.Types.Date;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using NodaTime;

namespace DataGen {
    public class Fetcher {
        private PhoneNumberGenerator PhoneNumberGenerator { get; }
        private NameFilter NameFilter { get; }
        private StringFilter Usernames { get; }
        private MailGenerator MailGenerator { get; }

        public Fetcher(NameFilter nameFilter = null, StringFilter usernames = null,
            MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? DataCollections.CountryCodes.Value.RandomItem;
            NameFilter = nameFilter ?? DataCollections.Names.Value;
            Usernames = usernames ?? DataCollections.UserNames.Value;
            MailGenerator = mailGenerator ?? new MailGenerator("gmail.com", "hotmail.com", "yahoo.com");
        }

        public string Name => NameFilter.RandomItem.Data;
        public string NameByType(NameTypes nameTypes) => NameFilter.ByType(nameTypes).RandomItem.Data;
        public string UserName => Usernames.RandomItem;
        public bool Bool => Number(2) != 0;
        public int Number(int maxNum) => HelperClass.Randomizer(maxNum);
        public int Number(int minNum, int maxNum) => HelperClass.Randomizer(minNum, maxNum);
        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);
        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        public string PhoneNumber(string preNumber = null, int length = 4) =>
            PhoneNumberGenerator.RandomNumber(length, preNumber);

        public string MailAdress(string name, string secondName = null) => MailGenerator.Mail(name, secondName);
    }
}