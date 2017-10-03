using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Randomizes strings representing user names from a <see cref="IReadOnlyList{T}" />.
    /// </summary>
    public class UserNameRandomizer : IUserNameProvider {
        private readonly Random _random;
        private readonly IReadOnlyList<string> _usernames;

        /// <summary>
        ///     Creates <see cref="UserNameRandomizer" />.
        /// </summary>
        public UserNameRandomizer(IReadOnlyList<string> usernames, Random random) {
            _usernames = usernames;
            _random = random;
        }

        /// <summary>
        ///     Randomizes a user name.
        /// </summary>
        /// <returns>
        ///     A string representing a user name.
        /// </returns>
        public string UserName() => _usernames.RandomItem(_random);
    }
}