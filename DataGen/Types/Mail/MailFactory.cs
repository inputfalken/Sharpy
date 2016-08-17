using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Mail {
    public class MailFactory {
        /// <summary>
        /// Will work as a history for each instance.
        /// </summary>
        private readonly List<string> _createdMails = new List<string>();

        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '.', '_', '-'
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
            if (text.Length == 0) throw new Exception("Argument cannot be zero");
            foreach (var emailDomain in _emailDomains) {
                if (text.Length == 1) {
                    var address = $"{text[0]}@{emailDomain}";
                    if (_createdMails.Contains(address)) continue;
                    _createdMails.Add(address);
                    return address;
                }
                //If length is not equal to 1 we start using separators
                foreach (var separator in Separators) {
                    var address = text.Aggregate((s, s1) => $"{s}{separator}{s1}") + $"@{emailDomain}";
                    if (_createdMails.Contains(address)) continue;
                    _createdMails.Add(address);
                    return address;
                }
            }
            throw new Exception("Could not create an unique mail");
        }
    }
}