﻿using System;
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
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, true);

            return UniqueEmailFactory(
                new StringBuilder(firstName.Length + second.Length)
                    .Append(first)
                    .Append(second)
            );
        }

        public string Mail(string firstName, string secondName, string thirdName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, true);

            return UniqueEmailFactory(
                new StringBuilder(first.Length + second.Length + third.Length)
                    .Append(first)
                    .Append(second)
                    .Append(third)
            );
        }

        public string Mail(string firstName, string secondName, string thirdName, string fourthName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, false);
            var fourth = BuildChars(fourthName, true);

            return UniqueEmailFactory(
                new StringBuilder(first.Length + second.Length + third.Length + fourth.Length)
                    .Append(first)
                    .Append(second)
                    .Append(third)
                    .Append(fourth)
            );
        }

        public string Mail(string firstName, string secondName, string thirdName, string fourthName, string fifthName)
        {
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, false);
            var fourth = BuildChars(fourthName, false);
            var fifth = BuildChars(fifthName, true);

            return UniqueEmailFactory(
                new StringBuilder(first.Length + second.Length + third.Length + fourth.Length + fifth.Length)
                    .Append(first)
                    .Append(second)
                    .Append(third)
                    .Append(fourth)
                    .Append(fifth)
            );
        }

        public string Mail(string name)
        {
            var first = BuildChars(name, true);
            return UniqueEmailFactory(
                new StringBuilder(first.Length)
                    .Append(first)
            );
        }

        public string Mail(
            string firstName,
            string secondName,
            string thirdName,
            string fourthName,
            string fifthName,
            params string[] names
        )
        {
            if (names.Length == 0)
                return Mail(firstName, secondName, thirdName, fourthName, fifthName);
            
            var first = BuildChars(firstName, false);
            var second = BuildChars(secondName, false);
            var third = BuildChars(thirdName, false);
            var fourth = BuildChars(fourthName, false);
            var fifth = BuildChars(fifthName, false);

            var sb = new StringBuilder(first.Length + second.Length + third.Length + fourth.Length + fifth.Length)
                .Append(first)
                .Append(second)
                .Append(third)
                .Append(fourth)
                .Append(fifth);


            for (var i = 0; i < names.Length; i++)
                sb.Append(BuildChars(names[i], i == names.Length - 1));

            return UniqueEmailFactory(sb);
        }

        public string Mail(string[] names)
        {
            if (names.Length == 0)
                throw new ArgumentException("Can not be empty.", nameof(names));

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < names.Length; i++)
                stringBuilder.Append(BuildChars(names[i], i == names.Length - 1));

            return UniqueEmailFactory(stringBuilder);
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

        private char[] BuildChars(string name, bool skipSeparator)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Invalid string value '{name}'.", nameof(name));

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