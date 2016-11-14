﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy.Implementation.Generators {
    /// <summary>
    /// </summary>
    internal sealed class MailGenerator : Unique<string> {
        private const int Limit = 2;

        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '.',
            '_',
            '-'
        };

        /// <summary>
        ///     Contains the email providers
        /// </summary>
        private readonly List<string> _emailDomains = new List<string>();

        /// <summary>
        ///     Contains the email providers but with saved state
        /// </summary>
        private readonly IEnumerator<string> _emailDomainsEnumerator;

        internal MailGenerator(IEnumerable<string> providers, Random random, bool unique) : base(random) {
            Unique = unique;
            foreach (var provider in providers) _emailDomains.Add(provider);
            _emailDomainsEnumerator = _emailDomains.GetEnumerator();
        }

        private bool Unique { get; }


        public string Mail(string name, string secondName) {
            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException("Argument must contain none null/empty string");
            name = name.ToLower();
            if (string.IsNullOrEmpty(secondName)) return Mail(name);
            secondName = secondName.ToLower();
            return Unique ? UniqueMail(name, secondName) : RandomMail(name, secondName);
        }

        private string RandomMail(string name, string secondname) => secondname == null
            ? name.Append("@").Append(_emailDomains[Random.Next(_emailDomains.Count)])
            : name.Append(Separators[Random.Next(Separators.Count)].ToString(), secondname, "@",
                _emailDomains[Random.Next(_emailDomains.Count)]);

        private string UniqueMail(string name, string secondName) {
            while (true) {
                var resets = 0;
                while (resets < Limit)
                    if (_emailDomainsEnumerator.MoveNext()) {
                        foreach (var separator in Separators) {
                            var address = name.Append(separator, secondName, "@", _emailDomainsEnumerator.Current);
                            if (HashSet.Contains(address)) continue;
                            HashSet.Add(address);
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
                secondName = OnDuplicate(secondName);
            }
        }


        private string Mail(string name) {
            while (true) {
                if (string.IsNullOrEmpty(name))
                    throw new NullReferenceException($"{nameof(name)} cannot be empty string or null");
                if (!Unique) return RandomMail(name, null);
                foreach (var emailDomain in _emailDomains) {
                    var address = name.Append("@", emailDomain);
                    if (HashSet.Contains(address)) continue;
                    HashSet.Add(address);
                    return address;
                }
                name = OnDuplicate(name);
            }
        }

        public override string ToString()
            => $"Providers: {_emailDomains.Aggregate((x, y) => $"{x}, {y}")} Unique addresses: {Unique}";

        private string OnDuplicate(string item) => item.Append(Random.Next(10));
    }
}