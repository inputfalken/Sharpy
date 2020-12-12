using System;
using System.Collections.Generic;
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

        public string Mail(string firstName, string secondName)
        {
            return UniqueEmailFactory(
                new StringBuilder()
                    .Append(BuildChars(firstName, true))
                    .Append(BuildChars(secondName, false))
            );
        }

        public string Mail(string firstName, string secondName, string thirdName)
        {
            return UniqueEmailFactory(
                new StringBuilder()
                    .Append(BuildChars(firstName, false))
                    .Append(BuildChars(secondName, false))
                    .Append(BuildChars(thirdName, true))
            );
        }

        private string UniqueEmailFactory(StringBuilder builder)
        {
            const int limit = 2;
            while (true)
            {
                var resets = 0;

                while (resets < limit)
                {
                    if (_domainsEnumerator.MoveNext())
                    {
                        var domain = _domainsEnumerator.Current;
                        var nameAndAtLength = builder.Length + 1;
                        var length = nameAndAtLength + domain.Length;
                        var chars = new char[length];

                        for (var i = 0; i < length; i++)
                            if (i < builder.Length) chars[i] = builder[i];
                            else if (i == builder.Length) chars[i] = '@';
                            else chars[i] = domain[i - nameAndAtLength];

                        var email = new string(chars, 0, chars.Length);

                        if (HashSet.Contains(email))
                            continue;
                        HashSet.Add(email);
                        return email;
                    }

                    _domainsEnumerator.Reset();
                    resets++;
                }

                // If emails are duplicated, we append numbers to the last element in names.
                builder.Append(Random.Next(10));
            }
        }

        public string Mail(string name)
        {
            return UniqueEmailFactory(new StringBuilder().Append(BuildChars(name, true)));
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
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < names.Length; i++)
            {
                stringBuilder.Append(BuildChars(names[i], i == names.Length - 1));
            }

            return UniqueEmailFactory(stringBuilder);
        }

        private char[] BuildChars(string name, bool skipSeparator)
        {
            _separatorEnumerator.MoveNext();
            var arr = new char[
                skipSeparator
                    ? name.Length
                    : name.Length + 1
            ];

            for (var i = 0; i < name.Length; i++)
                arr[i] = name[i];

            if (skipSeparator)
                return arr;

            arr[^1] = _separatorEnumerator.Current;
            return arr;
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