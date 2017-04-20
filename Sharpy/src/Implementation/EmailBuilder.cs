using System;
using System.Collections.Generic;
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


        internal EmailBuilder(IReadOnlyList<string> providers, Random random) : base(random) {
            EmailDomains =
                providers;
        }

        /// <summary>
        ///     Contains the email providers
        /// </summary>
        private IReadOnlyList<string> EmailDomains {
            set { EmailDomainsEnumerator = value.GetEnumerator(); }
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

        //todo restructure so the inner scopes don't have to do checkos for secondArgumetExists.
        private string UniqueMail(string name, string secondName) {
            var singleArgument = string.IsNullOrEmpty(secondName);
            while (true) {
                var resets = 0;
                while (resets < Limit)
                    if (EmailDomainsEnumerator.MoveNext()) {
                        foreach (var separator in Separators) {
                            var address = singleArgument
                                ? name.Append("@", EmailDomainsEnumerator.Current)
                                //Does not need to be in for each loop.
                                : name.Append(separator, secondName, "@", EmailDomainsEnumerator.Current);
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

                if (singleArgument) name = ResolveDuplicate(name);
                else secondName = ResolveDuplicate(secondName);
            }
        }


        private string ResolveDuplicate(string item) {
            return item.Append(Random.Next(10));
        }
    }
}