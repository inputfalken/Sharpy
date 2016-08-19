using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            _emailDomainsEnumerator = _emailDomains.GetEnumerator();
            _resetLimit = _emailDomains.Count * Separators.Count;
            _builder = new StringBuilder();
        }

        private readonly IEnumerator<string> _emailDomainsEnumerator;

        private readonly int _resetLimit;
        private readonly StringBuilder _builder;

        /// <summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can only be called up 3 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string Mail(string name, string secondName) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(secondName))
                throw new ArgumentException("Argument must contain none null/empty string");
            var resets = 0;
            while (true) {
                if (_emailDomainsEnumerator.MoveNext()) {
                    foreach (var separator in Separators) {
                        var address = _builder.Append(name)
                            .Append(separator)
                            .Append(secondName)
                            .Append("@")
                            .Append(_emailDomainsEnumerator.Current)
                            .ToString();
                        if (!_createdMails.Contains(address)) {
                            _createdMails.Add(address);
                            _builder.Clear();
                            return address;
                        }
                        _builder.Clear();
                    }
                }
                else {
                    _emailDomainsEnumerator.Reset();
                    resets += 1;
                    if (resets == _resetLimit)
                        throw new Exception("Could not create an unique mail");
                }
            }
        }


        ///<summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can only be called up 1 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        public string Mail(string name) {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name)} cannot be empty string or null");
            foreach (var emailDomain in _emailDomains) {
                var address = $"{name}@{emailDomain}";
                if (_createdMails.Contains(address)) continue;
                _createdMails.Add(address);
                return address;
            }
            throw new Exception("Could not create an unique mail");
        }
    }
}