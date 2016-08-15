using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Mail {
    public class MailFactory {
        /// <summary>
        ///     Since this is static it will not get created for each new instance of this object. Which means this can keep track
        ///     of all created mails.
        ///     Cons: List will be huge if use alot of mails are created.
        /// </summary>
        private static readonly List<string> CreatedMails = new List<string>();

        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '_', '.', '-'
        };

        private readonly List<string> _emailDomains = new List<string>();


        /// <summary>
        ///     Will use the strings as mail providers
        /// </summary>
        /// <param name="mailProviders">If Left Empty the mail providers will be defaulted to popular free providers.</param>
        public MailFactory(params string[] mailProviders) {
            if (mailProviders.Any())
                mailProviders.ForEach(_emailDomains.Add);
            else
                _emailDomains.AddRange(new[] { "yahoo.com", "gmail.com", "hotmail.com" });
        }


        /// <summary>
        ///     Returns a string representing a mail address.
        /// </summary>
        /// <param name="text">the string/strings used to construct a mail string</param>
        /// <returns></returns>
        public string Mail(params string[] text) {
            foreach (var i in Enumerable.Range(1, 10)) {
                var address =
                    $"{text.Aggregate((s, s1) => s + Separators[HelperClass.Randomizer(Separators.Count)] + s1).ToLower()}@{_emailDomains[HelperClass.Randomizer(_emailDomains.Count)]}";
                if (CreatedMails.Contains(address)) continue;
                CreatedMails.Add(address);
                return address;
            }
            throw new Exception("Reached maxium attempts to create unique Mails");
        }
    }
}