using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Builds unique email addresses.
    /// </summary>
    public sealed class UniqueEmailBuilder : UniqueRandomizer<string>, IEmailProvider
    {
        private const int Limit = 2;

        /// <summary>
        ///     Contains the email providers with saved state.
        /// </summary>
        private readonly IEnumerator<string> _domainsEnumerator;

        private readonly IEnumerator<char> _separatorEnumerator;

        internal UniqueEmailBuilder(IEnumerable<string> providers, Random random) : base(random)
        {
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
        public string Mail(params string[] names)
        {
            if (names.Length < 1) throw new ArgumentException($"Argument '{nameof(names)}' can not be empty.");
            while (true)
            {
                var resets = 0;

                while (resets < Limit)
                    if (_domainsEnumerator.MoveNext())
                    {
                        var mailBuilder = new StringBuilder();
                        for (var i = 0; i < names.Length; i++)
                        {
                            _separatorEnumerator.MoveNext();
                            var name = names[i];
                            if (string.IsNullOrEmpty(name))
                                throw new ArgumentNullException(
                                    $"No string in argument '{nameof(names)}' can be null or empty string."
                                );

                            var skipSeparator = i == names.Length - 1;
                            var arr = new char[skipSeparator
                                ? name.Length
                                : name.Length + 1];

                            for (var y = 0; y < name.Length; y++)
                                arr[y] = name[y];

                            if (skipSeparator)
                            {
                                mailBuilder.Append(arr);
                                continue;
                            }

                            arr[^1] = _separatorEnumerator.Current;

                            mailBuilder.Append(arr);
                        }

                        var mailAddress = mailBuilder
                            .Append('@')
                            .Append(_domainsEnumerator.Current)
                            .ToString();

                        if (HashSet.Contains(mailAddress)) continue;
                        HashSet.Add(mailAddress);
                        return mailAddress;
                    }
                    else
                    {
                        _domainsEnumerator.Reset();
                        resets++;
                    }

                // If emails are duplicated, we append numbers to the last element in names.
                names[^1] = names[^1] + Random.Next(10);
            }
        }

        /// <summary>
        ///     Creates an email with a randomized user name.
        /// </summary>
        /// <returns></returns>
        public string Mail()
        {
            while (true)
            {
                var randomItem = Random.ListElement(Data.GetUserNames);
                if (randomItem.Length < 4) continue;
                return Mail(randomItem);
            }
        }

        private static IEnumerable<char> Infinite()
        {
            while (true)
            {
                yield return '.';
                yield return '_';
                yield return '-';
            }
        }
    }
}