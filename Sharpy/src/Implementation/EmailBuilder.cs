using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy.Implementation {
    /// <summary>
    /// </summary>
    internal sealed class EmailBuilder : Unique<string> {
        private const int Limit = 2;

        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '.',
            '_',
            '-'
        };

        private IReadOnlyList<string> _emailDomains = new List<string>();


        internal EmailBuilder(IReadOnlyList<string> providers, Random random) : base(random) {
            EmailDomains = providers;
        }

        /// <summary>
        ///     Contains the email providers
        /// </summary>
        private IReadOnlyList<string> EmailDomains {
            get { return _emailDomains; }
            set {
                EmailDomainsEnumerator = value.GetEnumerator();
                _emailDomains = value;
            }
        }

        /// <summary>
        ///     Contains the email providers but with saved state
        /// </summary>
        private IEnumerator<string> EmailDomainsEnumerator { get; set; }


        public string Mail(string name, string secondName) {
            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException($"{nameof(name)} can't be null/empty string");
            return UniqueMail(name, secondName).ToLower();
        }

        private string UniqueMail(string name, string secondName) {
            while (true) {
                var resets = 0;
                while (resets < Limit)
                    if (EmailDomainsEnumerator.MoveNext()) {
                        foreach (var separator in Separators) {
                            var address = secondName != null
                                ? name.Append(separator, secondName, "@", EmailDomainsEnumerator.Current)
                                : name.Append("@", EmailDomainsEnumerator.Current);
                            if (HashSet.Contains(address)) continue;
                            HashSet.Add(address);
                            return address;
                        }
                    }
                    else {
                        //If this needs to be called more than once.
                        //It means that every combination with separators and EmailDomains has been iterated.
                        EmailDomainsEnumerator.Reset();
                        resets += 1;
                    }
                // Start adding numbers.
                secondName = ResolveDuplicate(secondName);
            }
        }


        private string ResolveDuplicate(string item) => item.Append(Random.Next(10));
    }
}