using DataGen.Types.CountryCode;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;

namespace DataGen {
    public class Config {
        public PhoneNumberGenerator PhoneNumberGenerator { get; set; }
        public NameFilter NameFilter { get; set; }
        public StringFilter Usernames { get; set; }
        public MailGenerator MailGenerator { get; set; }

        public Config(NameFilter nameFilter = null, StringFilter usernames = null,
            MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? DataCollections.CountryCodes.Value.RandomItem;
            NameFilter = nameFilter ?? DataCollections.Names.Value;
            Usernames = usernames ?? DataCollections.UserNames.Value;
            MailGenerator = mailGenerator ?? new MailGenerator("gmail.com", "hotmail.com", "yahoo.com");
        }
    }
}