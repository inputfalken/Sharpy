using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;

namespace DataGen {
    public class Person {
        private static readonly NameFilter NameFilter = DataCollections.CommonNames.Value.ByCountry("sweden");
        private static readonly StringFilter UserNameFilter = DataCollections.CommonUserNames.Value.ByLength(8);
        private static readonly MailFactory MailFactory = new MailFactory();

        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
        public string Username { get; }

        public Person() {
            FirstName = NameFilter.ByType(NameTypes.MixedNames).RandomItem.Data;
            LastName = NameFilter.ByType(NameTypes.LastNames).RandomItem.Data;
            Username = UserNameFilter.RandomItem;
            MailAddress = MailFactory.Mail(FirstName, LastName);
        }

        public override string ToString() {
            return $"Name: {FirstName} {LastName} \nMail: {MailAddress}\nUser Name: {Username}";
        }
    }
}