﻿using System;
using System.Collections.Generic;

namespace Sharpy.Types.Mail {
    /// <summary>
    /// 
    /// </summary>
    public class MailGenerator : Unique<string> {
        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '.', '_', '-'
        };

        /// <summary>
        ///     Contains the email providers
        /// </summary>
        private readonly List<string> _emailDomains = new List<string>();

        /// <summary>
        ///     Contains the email providers but with saved state
        /// </summary>
        private readonly IEnumerator<string> _emailDomainsEnumerator;


        /// <summary>
        ///     Will use the strings as mail providers
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="random"></param>
        /// <param name="unique"></param>
        internal MailGenerator(IEnumerable<string> providers, Random random, bool unique) : base(2, random) {
            Unique = unique;
            providers.ForEach(_emailDomains.Add);
            _emailDomainsEnumerator = _emailDomains.GetEnumerator();
        }

        private bool Unique { get; }


        /// <summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can be called up 3 * ammount of mail suppliers with the same argument. After that it will
        ///     throw an exception
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string Mail(string name, string secondName) {
            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException("Argument must contain none null/empty string");
            if (string.IsNullOrEmpty(secondName)) return Mail(name);

            return Unique ? UniqueMail(name, secondName) : RandomMail(name, secondName);
        }

        /// <summary>
        ///     Gives a mail address with randomed separator and domain
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        private string RandomMail(params string[] strings) {
            foreach (var name in strings) {
                Builder.Append(name);
                if (name == strings[strings.Length - 1])
                    Builder.Append("@").Append(_emailDomains[Random.Next(_emailDomains.Count)]);
                else
                    Builder.Append(Separators[Random.Next(Separators.Count)].ToString());
            }
            var address = Builder.ToString().ToLower();
            Builder.Clear();
            return address;
        }

        /// <summary>
        ///     Will try to create an unique mail address
        ///     If all possible combinations for the arguments used it will throw an exception
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string UniqueMail(string name, string secondName) {
            while (true) {
                var resets = 0;
                while (resets < AttemptLimit)
                    if (_emailDomainsEnumerator.MoveNext()) {
                        foreach (var separator in Separators) {
                            var address = BuildString(name, separator.ToString(), secondName, "@",
                                _emailDomainsEnumerator.Current);
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
                // Start adding numbers.
                secondName = secondName + Random.Next(9);
            }
        }

        private static string BuildString(params string[] strings) {
            foreach (var s in strings)
                Builder.Append(s);
            return Builder.ToString().ToLower();
        }

        /// <summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can only be called up 1 * ammount of mail suppliers with the same argument. After that it
        ///     will throw an exception
        /// </summary>
        public string Mail(string name) {
            while (true) {
                if (string.IsNullOrEmpty(name))
                    throw new NullReferenceException($"{nameof(name)} cannot be empty string or null");
                if (!Unique) return RandomMail(name);
                foreach (var emailDomain in _emailDomains) {
                    var address = BuildString(name, "@", emailDomain);
                    if (ClearValidateSave(address))
                        return address;
                }
                // Start adding numbers
                name = name + Random.Next(9);
            }
        }
    }
}