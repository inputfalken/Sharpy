using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Builds unique email addresses.
    /// </summary>
    public sealed class UniqueEmailBuilder : UniqueRandomizer<string>, IEmailProvider {
        private const int Limit = 2;

        /// <summary>
        ///     Contains the email providers with saved state.
        /// </summary>
        private readonly IEnumerator<string> _domainsEnumerator;

        private readonly IEnumerator<char> _separatorEnumerator;

        internal UniqueEmailBuilder(IEnumerable<string> providers, Random random) : base(random) {
            _domainsEnumerator = providers.GetEnumerator();
            _separatorEnumerator = Infinite().GetEnumerator();
        }

        /// <summary>
        ///     Creates an email by joining <paramref name="names" /> with common separator characters.
        /// </summary>
        /// <param name="names">
        ///     The names to be joined.
        /// </param>
        /// <returns>
        ///     A string representing a email address.
        /// </returns>
        public string Mail(params string[] names) {
            if (names == null) throw new ArgumentNullException(nameof(names));
            if (names.Length < 1) throw new ArgumentException($"Argument '{nameof(names)}' can not be empty.");
            var resets = 0;
            var namesWithIndex = names
                .Select((s, i) => (
                    name: string.IsNullOrEmpty(s)
                        ? throw new ArgumentNullException(
                            $"No string in argument '{nameof(names)}' can be null or empty string.")
                        : s,
                    iteration: i)
                ).ToArray();

            while (resets < Limit)
                if (_domainsEnumerator.MoveNext()) {
                    var mailAddress = namesWithIndex.Aggregate(string.Empty,
                        (acc, curr) => {
                            _separatorEnumerator.MoveNext();
                            return
                                $"{acc}{(curr.iteration == names.Length - 1 ? curr.name : curr.name.Append(_separatorEnumerator.Current.ToString()))}";
                        },
                        result => result.Append('@', _domainsEnumerator.Current).ToLower()
                    );

                    if (!HashSet.Contains(mailAddress)) {
                        HashSet.Add(mailAddress);
                        return mailAddress;
                    }
                }
                else {
                    _domainsEnumerator.Reset();
                    resets++;
                }
            names[names.Length - 1] = ResolveDuplicate(names[names.Length - 1]);
            return Mail(names);
        }

        /// <summary>
        ///     Creates an email with a randomized user name.
        /// </summary>
        /// <returns></returns>
        public string Mail() {
            while (true) {
                var randomItem = Data.GetUserNames.RandomItem(Random);
                if (randomItem.Length < 4) continue;
                return Mail(randomItem);
            }
        }

        private static IEnumerable<char> Infinite() {
            while (true) {
                yield return '.';
                yield return '_';
                yield return '-';
            }
        }

        private string ResolveDuplicate(string item) => item.Append(Random.Next(10));
    }
}