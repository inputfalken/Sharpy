using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpy.Core;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy.Implementation {
    internal sealed class EmailBuilder : Unique<string>, IEmailProvider {
        private const int Limit = 2;

        private readonly IGenerator<char> _seperatorEnumerator;

        internal EmailBuilder(IReadOnlyList<string> providers, Random random) : base(random) {
            EmailDomains = providers;
            _seperatorEnumerator = Generator.CircularSequence(new List<char> {
                '.',
                '_',
                '-'
            });
        }

        /// <summary>
        ///     Contains the email providers
        /// </summary>
        private IReadOnlyList<string> EmailDomains {
            set => DomainsEnumerator = value.GetEnumerator();
        }

        /// <summary>
        ///     Contains the email providers but with saved state
        /// </summary>
        private IEnumerator<string> DomainsEnumerator { get; set; }

        private string ResolveDuplicate(string item) => item.Append(Random.Next(10));

        public string Mail(params string[] names) {
            if (names == null) throw new ArgumentNullException(nameof(names));
            if (names.Length < 1) throw new ArgumentException($"Argument '{nameof(names)}' can not be empty.");
            var resets = 0;
            var namesWithIndex = names
                .Select((s, i) => (name: string.IsNullOrEmpty(s)
                    ? throw new ArgumentNullException(
                        $"No element in argument '{nameof(names)}' can be null or empty string.")
                    : s, iteration: i)).ToArray();

            while (resets < Limit) {
                if (DomainsEnumerator.MoveNext()) {
                    var result = namesWithIndex.Aggregate(string.Empty,
                            (acc, curr) =>
                                $"{acc}{(curr.iteration == names.Length - 1 ? curr.name : curr.name.Append(_seperatorEnumerator.Generate().ToString()))}"
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

    public interface IEmailProvider {
        string Mail(params string[] name);
    }
}