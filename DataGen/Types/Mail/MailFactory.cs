﻿using System;
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
        private static readonly IEnumerable<char> Separators = new List<char> {
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
        ///     This method currently can only be called up 3 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        /// <param name="names">the string/strings used to construct a mail string</param>
        /// <returns></returns>
        public string Mail(params string[] names) {
            if (names.Length == 0)
                throw new ArgumentException("This methods cannot be called with out arguments");
            if (names.Any(string.IsNullOrEmpty))
                throw new ArgumentException($"{nameof(names)} must contain none null/empty string");

            foreach (var emailDomain in _emailDomains) {
                foreach (var separator in Separators) {
                    var address = names.Aggregate((s, s1) => $"{s}{separator}{s1}") + $"@{emailDomain}";
                    if (_createdMails.Contains(address)) continue;
                    _createdMails.Add(address);
                    return address;
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