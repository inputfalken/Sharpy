using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGen.Types.Mail {
    public class MailFactory : Unique<string> {
        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '.', '_', '-'
        };

        ///<summary>
        ///     Contains the email providers
        /// </summary>
        private readonly List<string> _emailDomains = new List<string>();


        /// <summary>
        ///     Will use the strings as mail providers
        /// </summary>
        /// <param name="mailProviders">If Left Empty the mail providers will be defaulted to popular free providers.</param>
        public MailFactory(params string[] mailProviders) : base(2) {
            if (mailProviders.Any())
                mailProviders.ForEach(_emailDomains.Add);
            else
                _emailDomains.AddRange(new[] { "yahoo.com", "gmail.com", "hotmail.com" });
            _emailDomainsEnumerator = _emailDomains.GetEnumerator();
        }

        ///<summary>
        ///     Contains the email providers but with saved state
        /// </summary>
        private readonly IEnumerator<string> _emailDomainsEnumerator;


        /// <summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can be called up 3 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string Mail(string name, string secondName) {
            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException("Argument must contain none null/empty string");
            if (string.IsNullOrEmpty(secondName)) return Mail(name);
            var resets = 0;
            while (resets < AttemptLimit) {
                if (_emailDomainsEnumerator.MoveNext()) {
                    foreach (var separator in Separators) {
                        var address = Builder.Append(name)
                            .Append(separator)
                            .Append(secondName)
                            .Append("@")
                            .Append(_emailDomainsEnumerator.Current)
                            .ToString()
                            .ToLower();
                        if (ClearValidateSave(address))
                            return address;
                    }
                }
                else {
                    //If this needs to be called more than once.
                    //It means that every combination with separators and EmailDomains has been iterated.
                    _emailDomainsEnumerator.Reset();
                    resets += 1;
                }
            }
            throw new Exception("Could not create an unique mail");
        }


        ///<summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can only be called up 1 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        public string Mail(string name) {
            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException($"{nameof(name)} cannot be empty string or null");
            foreach (var emailDomain in _emailDomains) {
                var address = Builder.Append(name)
                    .Append("@")
                    .Append(emailDomain)
                    .ToString()
                    .ToLower();
                if (ClearValidateSave(address))
                    return address;
            }
            throw new Exception("Could not create an unique mail");
        }
    }
}