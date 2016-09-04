using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DataGen.Types.HelperClass;

namespace DataGen.Types.Mail {
    public class MailGenerator : Unique<string> {
        private bool Unique { get; }

        /// <summary>
        ///     Used for separating strings with symbols
        /// </summary>
        private static readonly List<char> Separators = new List<char> {
            '.', '_', '-'
        };

        ///<summary>
        ///     Contains the email providers
        /// </summary>
        private readonly List<string> _emailDomains = new List<string>();


        /// <summary>
        ///     Will use the strings as mail providers
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="unique"></param>
        public MailGenerator(IEnumerable<string> providers, bool unique) : base(2) {
            Unique = unique;
            providers.ForEach(_emailDomains.Add);
            _emailDomainsEnumerator = _emailDomains.GetEnumerator();
        }

        ///<summary>
        ///     Contains the email providers but with saved state
        /// </summary>
        private readonly IEnumerator<string> _emailDomainsEnumerator;


        /// <summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can be called up 3 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string Mail(string name, string secondName) {
            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException("Argument must contain none null/empty string");
            if (string.IsNullOrEmpty(secondName)) return Mail(name);

            return Unique ? UniqueMail(name, secondName) : RandomMail(name, secondName);
        }

        /// <summary>
        ///    Gives a mail address with randomed separator and domain
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        private string RandomMail(params string[] strings) {
            foreach (var name in strings) {
                Builder.Append(name);
                if (name == strings[strings.Length - 1])
                    Builder.Append("@").Append(_emailDomains[SetRandomizer(_emailDomains.Count)]);
                else
                    Builder.Append(Separators[SetRandomizer(Separators.Count)].ToString());
            }
            var address = Builder.ToString().ToLower();
            Builder.Clear();
            return address;
        }

        /// <summary>
        ///    Will try to create an unique mail address
        ///    If all possible combinations for the arguments used it will throw an exception
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string UniqueMail(string name, string secondName) {
            while (true) {
                var resets = 0;
                while (resets < AttemptLimit)
                    if (_emailDomainsEnumerator.MoveNext()) {
                        foreach (var separator in Separators) {
                            var address = BuildString(name, separator.ToString(), secondName, "@",
                                _emailDomainsEnumerator.Current);
                            if (ClearValidateSave(address))
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
                secondName = secondName + SetRandomizer(9);
            }
        }

        private static string BuildString(params string[] strings) {
            foreach (var s in strings)
                Builder.Append(s);
            return Builder.ToString().ToLower();
        }

        ///<summary>
        ///     Returns a string representing a mail address.
        ///     This method currently can only be called up 1 * ammount of mail suppliers with the same argument. After that it will throw an exception
        /// </summary>
        public string Mail(string name) {
            while (true) {
                if (string.IsNullOrEmpty(name))
                    throw new NullReferenceException($"{nameof(name)} cannot be empty string or null");
                if (!Unique) return RandomMail(name);
                foreach (var emailDomain in _emailDomains) {
                    var address = BuildString(name, "@", emailDomain);
                    if (ClearValidateSave(address))
                        return address;
                }
                // Start adding numbers
                name = name + SetRandomizer(9);
            }
        }
    }
}