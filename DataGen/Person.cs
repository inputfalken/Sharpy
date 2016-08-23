using System.Linq;
using DataGen.Types;
using DataGen.Types.CountryCode;
using DataGen.Types.Date;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using NodaTime;

namespace DataGen {
    public class Person {
        private const string Country = "sweden";
        private static readonly NameFilter NameFilter = DataCollections.CommonNames.Value.ByCountry(Country);

        private static readonly CountryCode CountryCode =
            DataCollections.CountryCodeFilters.Value.ByCountry(Country).First();

        private static readonly StringFilter UserNameFilter = DataCollections.CommonUserNames.Value.ByLength(8);
        private static readonly MailFactory MailFactory = new MailFactory();

        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
        //Todo make it optional if user wants usernames unique
        public string Username { get; }
        //Todo make every phone number unique
        public string PhoneNumber { get; }
        public LocalDate DateOfBirth { get; }

        public Person() {
            FirstName = NameFilter.ByType(NameTypes.MixedFirstNames).RandomItem.Data;
            LastName = NameFilter.ByType(NameTypes.LastNames).RandomItem.Data;
            Username = UserNameFilter.RandomItem;
            MailAddress = MailFactory.Mail(FirstName, LastName);
            PhoneNumber = CountryCode.CreateRandomNumber(4, "39");
            DateOfBirth = DateGenerator.RandomDateByAge(HelperClass.Randomizer(15, 45));
        }

        public override string ToString() {
            return
                $"Name: {FirstName} {LastName}\nPhone: {PhoneNumber} \nMail: {MailAddress}\nUser Name: {Username}\nDate Of Birth: {DateOfBirth}";
        }
    }
}