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

        private static readonly char[] Separators = { '.', '_', '-' };
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
            var first = BuildSeparatedString(in firstName);
            const int includedSeparatorLength = 1;

            var emailLength = first.Length
                              + secondName.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(first)
                    .Append(secondName)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName)
        {
            var first = BuildSeparatedString(in firstName);
            var second = BuildSeparatedString(in secondName);
            const int includedSeparatorLength = 2;

            var emailLength = first.Length
                              + second.Length
                              + thirdName.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(first)
                    .Append(second)
                    .Append(thirdName)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName)
        {
            var first = BuildSeparatedString(in firstName);
            var second = BuildSeparatedString(in secondName);
            var third = BuildSeparatedString(in thirdName);
            const int includedSeparatorLength = 3;
            var emailLength = first.Length
                              + second.Length
                              + third.Length
                              + fourthName.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(first)
                    .Append(second)
                    .Append(third)
                    .Append(fourthName)
            );
        }

        /// <inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName,
            in string fifthName)
        {
            var first = BuildSeparatedString(in firstName);
            var second = BuildSeparatedString(in secondName);
            var third = BuildSeparatedString(in thirdName);
            var fourth = BuildSeparatedString(in fourthName);

            const int includedSeparatorLength = 4;
            var emailLength = first.Length
                              + second.Length
                              + third.Length
                              + fourth.Length
                              + fifthName.Length
                              + AtLength
                              + includedSeparatorLength
                              + _infiniteDomainEnumerator.Current.Length;
            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(first)
                    .Append(second)
                    .Append(third)
                    .Append(fourth)
                    .Append(fifthName)
            );
        }

        /// <inheritdoc />
        public string Mail(in string name)
        {
            var emailLength = name.Length
                              + AtLength
                              + _infiniteDomainEnumerator.Current.Length;

            return UniqueEmailFactory(
                new StringBuilder(emailLength, emailLength)
                    .Append(name)
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
                return Mail(in firstName, in secondName, in thirdName, in fourthName, in fifthName);

            var first = BuildSeparatedString(in firstName);
            var second = BuildSeparatedString(in secondName);
            var third = BuildSeparatedString(in thirdName);
            var fourth = BuildSeparatedString(in fourthName);
            var fifth = BuildSeparatedString(in fifthName);
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
                sb.Append(i == additional.Length - 1 ? additional[i] : BuildSeparatedString(additional[i]));

            return UniqueEmailFactory(sb);
        }

        /// <inheritdoc />
        public string Mail(in string[] names)
        {
            if (names.Length == 0)
                throw new ArgumentException("Can not be empty.", nameof(names));

            // We can not know the final string length due to the array without looping through it.
            var minimumEmailLength = AtLength + _infiniteDomainEnumerator.Current.Length + names.Length;

            var sb = new StringBuilder(minimumEmailLength);
            for (var i = 0; i < names.Length; i++)
                sb.Append(i == names.Length - 1 ? names[i] : BuildSeparatedString(in names[i]));

            return UniqueEmailFactory(sb);
        }

        private string UniqueEmailFactory(in StringBuilder builder)
        {
            var atIndex = builder.Length;
            string email = builder
                .Append('@')
                .Append(_infiniteDomainEnumerator.Current)
                .ToString();

            if (_dictionary.TryGetValue(email, out var count))
            {
                _dictionary[email] = ++count;
                // On duplicated emails, we append the count number to ensure that we do not get duplicated emails.
                _infiniteDomainEnumerator.MoveNext();
                return email.Insert(atIndex, count.ToString());
            }

            _dictionary.Add(email, 0);
            _infiniteDomainEnumerator.MoveNext();
            return email;
        }

        private string BuildSeparatedString(in string name)
        {
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