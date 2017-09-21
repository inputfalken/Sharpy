﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Core;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    /// Builds unique email addresses .
    /// </summary>
    public sealed class UniqueEmailBuilder : UniqueRandomizer<string>, IEmailProvider {
        private const int Limit = 2;

        private readonly IGenerator<char> _separatorGenerator;

        internal UniqueEmailBuilder(IReadOnlyList<string> providers, Random random) : base(random) {
            EmailDomains = providers;
            _separatorGenerator = Generator.CircularSequence(new List<char> {
                '.',
                '_',
                '-'
            });
        }

        /// <summary>
        ///     Contains the email providers.
        /// </summary>
        private IReadOnlyList<string> EmailDomains {
            set => DomainsEnumerator = value.GetEnumerator();
        }

        /// <summary>
        ///     Contains the email providers with saved state.
        /// </summary>
        private IEnumerator<string> DomainsEnumerator { get; set; }

        private string ResolveDuplicate(string item) => item.Append(Random.Next(10));

        /// <inheritdoc />
        public string Mail(params string[] names) {
            if (names == null) throw new ArgumentNullException(nameof(names));
            if (names.Length < 1) throw new ArgumentException($"Argument '{nameof(names)}' can not be empty.");
            var resets = 0;
            var namesWithIndex = names
                .Select((s, i) => (
                    name: string.IsNullOrEmpty(s)
                        ? throw new ArgumentNullException(
                            $"No element in argument '{nameof(names)}' can be null or empty string.")
                        : s, iteration: i)
                ).ToArray();

            while (resets < Limit) {
                if (DomainsEnumerator.MoveNext()) {
                    var result = namesWithIndex.Aggregate(string.Empty,
                            (acc, curr) =>
                                $"{acc}{(curr.iteration == names.Length - 1 ? curr.name : curr.name.Append(_separatorGenerator.Generate().ToString()))}"
                        ).Append('@', DomainsEnumerator.Current)
                        .ToLower();

                    if (!HashSet.Contains(result)) {
                        HashSet.Add(result);
                        return result;
                    }
                }
                else {
                    DomainsEnumerator.Reset();
                    resets++;
                }
            }
            names[names.Length - 1] = ResolveDuplicate(names[names.Length - 1]);
            return Mail(names);
        }
    }
}