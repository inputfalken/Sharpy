using System.Linq;
using DataGen.Types.CountryCode;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;

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
        public string Username { get; }
        public string PhoneNumber { get; }

        public Person() {
            FirstName = NameFilter.ByType(NameTypes.MixedNames).RandomItem.Data;
            LastName = NameFilter.ByType(NameTypes.LastNames).RandomItem.Data;
            Username = UserNameFilter.RandomItem;
            MailAddress = MailFactory.Mail(FirstName, LastName);
            PhoneNumber = CountryCode.CreateRandomNumber(4, "39");
        }

        public override string ToString() {
            return $"Name: {FirstName} {LastName}\nPhone: {PhoneNumber} \nMail: {MailAddress}\nUser Name: {Username}";
        }
    }
}