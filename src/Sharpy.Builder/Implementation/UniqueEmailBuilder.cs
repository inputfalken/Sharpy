using System;
using System.Collections.Generic;
using System.Text;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Builds unique email addresses.
    /// </summary>
    public sealed class UniqueEmailBuilder : IEmailProvider
    {
        private const int AtLength = 1;

        private static readonly char[] Separators = {'.', '_', '-'};
        private readonly IDictionary<string, int> _dictionary;

        /// <summary>
        ///     Contains the email providers with saved state.
        /// </summary>
        private readonly IEnumerator<string> _infiniteDomainEnumerator;


        private readonly IEnumerator<char> _separatorEnumerator;

        internal UniqueEmailBuilder(IReadOnlyList<string> providers)
        {
            _infiniteDomainEnumerator = Infinite(providers).GetEnumerator();
            _separatorEnumerator = Infinite(Separators).GetEnumerator();
            _dictionary = new Dictionary<string, int>();

            // Start the enumerator in order to predict the upcoming length.
            _infiniteDomainEnumerator.MoveNext();
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName)
        {
            var first = BuildSeparatedString(firstName, false);
            var second = BuildSeparatedString(secondName, true);
            const int includedSeparatorLength = 1;

            var emailLength = firstName.Length
                              + second.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(first)
                    .Append(second)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName)
        {
            var first = BuildSeparatedString(firstName, false);
            var second = BuildSeparatedString(secondName, false);
            var third = BuildSeparatedString(thirdName, true);
            const int includedSeparatorLength = 2;

            var emailLength = first.Length
                              + second.Length
                              + third.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(first)
                    .Append(second)
                    .Append(third)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName)
        {
            var first = BuildSeparatedString(firstName, false);
            var second = BuildSeparatedString(secondName, false);
            var third = BuildSeparatedString(thirdName, false);
            var fourth = BuildSeparatedString(fourthName, true);
            const int includedSeparatorLength = 3;
            var emailLength = first.Length
                              + second.Length
                              + third.Length
                              + fourth.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
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
            var first = BuildSeparatedString(firstName, false);
            var second = BuildSeparatedString(secondName, false);
            var third = BuildSeparatedString(thirdName, false);
            var fourth = BuildSeparatedString(fourthName, false);
            var fifth = BuildSeparatedString(fifthName, true);

            const int includedSeparatorLength = 4;
            var emailLength = first.Length
                              + second.Length
                              + third.Length
                              + fourth.Length
                              + fifth.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;
            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
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
            var first = BuildSeparatedString(name, true);
            var emailLength = first.Length
                              + AtLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
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

            var first = BuildSeparatedString(firstName, false);
            var second = BuildSeparatedString(secondName, false);
            var third = BuildSeparatedString(thirdName, false);
            var fourth = BuildSeparatedString(fourthName, false);
            var fifth = BuildSeparatedString(fifthName, false);
            var includedSeparatorLength = 5 + additional.Length;

            // We can not know the final string length due to the array without looping through it.
            var minimumEmailLength = first.Length
                                     + second.Length
                                     + third.Length
                                     + fourth.Length
                                     + fifth.Length
                                     + AtLength
                                     + includedSeparatorLength
                                     + _infiniteDomainEnumerator.Current.Length
                                     + additional.Length;

            var sb = new StringBuilder(minimumEmailLength)
                .Append(first)
                .Append(second)
                .Append(third)
                .Append(fourth)
                .Append(fifth);


            for (var i = 0; i < additional.Length; i++)
                sb.Append(BuildSeparatedString(additional[i], i == additional.Length - 1));

            return UniqueEmailFactory(sb);
        }

        /// <inheritdoc />
        public string Mail(in string[] names)
        {
            if (names.Length == 0)
                throw new ArgumentException("Can not be empty.", nameof(names));

            // We can not know the final string length due to the array without looping through it.
            var minimumEmailLength = AtLength + _infiniteDomainEnumerator.Current.Length + names.Length;
            
            var stringBuilder = new StringBuilder(minimumEmailLength);
            for (var i = 0; i < names.Length; i++)
                stringBuilder.Append(BuildSeparatedString(names[i], i == names.Length - 1));

            return UniqueEmailFactory(stringBuilder);
        }

        private string UniqueEmailFactory(in StringBuilder builder)
        {
            string email = builder
                .Append('@')
                .Append(_infiniteDomainEnumerator.Current)
                .ToString();

            if (_dictionary.TryGetValue(email, out var count))
            {
                _dictionary[email] = ++count;
                // On duplicated emails, we append the count number to ensure that we do not get duplicated emails.
                _infiniteDomainEnumerator.MoveNext();
                return email.Insert(builder.Length, count.ToString());
            }

            _dictionary.Add(email, 0);
            _infiniteDomainEnumerator.MoveNext();
            return email;
        }

        private string BuildSeparatedString(in string name, in bool skipSeparator)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Invalid string value '{name}'.", nameof(name));
            
            if (skipSeparator) return name;
            _separatorEnumerator.MoveNext();

            return string.Concat(name, _separatorEnumerator.Current);
        }

        private static IEnumerable<T> Infinite<T>(IReadOnlyList<T> list)
        {
            while (true)
                foreach (var element in list)
                    yield return element;
        }
    }
}