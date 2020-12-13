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
    public sealed class UniqueEmailBuilder : IEmailProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Contains the email providers with saved state.
        /// </summary>
        private readonly IEnumerator<string> _infiniteDomainEnumerator;

        private const int AtLength = 1;

        private static readonly char[] Separators = {'.', '_', '-'};
        private readonly IDictionary<string, int> _dictionary;

        private readonly IEnumerator<char> _separatorEnumerator;

        internal UniqueEmailBuilder(IReadOnlyList<string> providers, Random random)
        {
            _random = random;
            _infiniteDomainEnumerator = Infinite(providers).GetEnumerator();
            _separatorEnumerator = Infinite(Separators).GetEnumerator();
            _dictionary = new Dictionary<string, int>();
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, true);

            return UniqueEmailFactory(
                new StringBuilder(
                        firstName.Length
                        + second.Length
                        + AtLength
                    )
                    .Append(first)
                    .Append(second)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, true);

            return UniqueEmailFactory(
                new StringBuilder(
                        first.Length
                        + second.Length
                        + third.Length
                        + AtLength
                    )
                    .Append(first)
                    .Append(second)
                    .Append(third)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, false);
            var fourth = BuildChars(fourthName, true);

            return UniqueEmailFactory(
                new StringBuilder(
                        first.Length
                        + second.Length
                        + third.Length
                        + fourth.Length
                        + AtLength
                    )
                    .Append(first)
                    .Append(second)
                    .Append(third)
                    .Append(fourth)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName,
            in string fifthName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, false);
            var fourth = BuildChars(fourthName, false);
            var fifth = BuildChars(fifthName, true);

            return UniqueEmailFactory(
                new StringBuilder(
                        first.Length
                        + second.Length
                        + third.Length
                        + fourth.Length
                        + fifth.Length
                        + AtLength
                    )
                    .Append(first)
                    .Append(second)
                    .Append(third)
                    .Append(fourth)
                    .Append(fifth)
            );
        }

        /// <inheritdoc />
        public string Mail(in string name)
        {
            var first = BuildChars(name, true);
            return UniqueEmailFactory(
                new StringBuilder(
                        first.Length
                        + AtLength
                    )
                    .Append(first)
            );
        }

        /// <inheritdoc />
        public string Mail(
            in string firstName,
            in string secondName,
            in string thirdName,
            in string fourthName,
            in string fifthName,
            params string[] additional
        )
        {
            if (additional.Length == 0)
                return Mail(firstName, secondName, thirdName, fourthName, fifthName);

            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, false);
            var fourth = BuildChars(fourthName, false);
            var fifth = BuildChars(fifthName, false);

            var sb = new StringBuilder(
                    first.Length
                    + second.Length
                    + third.Length
                    + fourth.Length
                    + fifth.Length
                    + AtLength
                )
                .Append(first)
                .Append(second)
                .Append(third)
                .Append(fourth)
                .Append(fifth);


            for (var i = 0; i < additional.Length; i++)
                sb.Append(BuildChars(additional[i], i == additional.Length - 1));

            return UniqueEmailFactory(sb);
        }

        /// <inheritdoc />
        public string Mail(in string[] names)
        {
            if (names.Length == 0)
                throw new ArgumentException("Can not be empty.", nameof(names));

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < names.Length; i++)
                stringBuilder.Append(BuildChars(names[i], i == names.Length - 1));

            return UniqueEmailFactory(stringBuilder);
        }

        /// <inheritdoc />
        public string Mail()
        {
            while (true)
            {
                var randomItem = _random.ListElement(Data.GetUserNames);
                if (randomItem.Length < 4) continue;
                return Mail(randomItem);
            }
        }

        private string UniqueEmailFactory(in StringBuilder builder)
        {
            _infiniteDomainEnumerator.MoveNext();

            string email = builder
                .Append('@')
                .Append(_infiniteDomainEnumerator.Current)
                .ToString();

            if (_dictionary.TryGetValue(email, out var count))
            {
                _dictionary[email] = ++count;
                // On duplicated emails, we append the count number to ensure that we do not get duplicated emails.
                return email.Insert(builder.Length, count.ToString());
            }

            _dictionary.Add(email, 0);
            return email;
        }

        private string BuildChars(in string name, in bool skipSeparator)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Invalid string value '{name}'.", nameof(name));

            if (skipSeparator) return name;
            _separatorEnumerator.MoveNext();
            return name + _separatorEnumerator.Current;

        }

        private static IEnumerable<T> Infinite<T>(IReadOnlyList<T> list)
        {
            while (true)
            {
                foreach (var element in list)
                {
                    yield return element;
                }
            }
        }
    }
}