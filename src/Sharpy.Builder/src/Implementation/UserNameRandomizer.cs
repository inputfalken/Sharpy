using System;
using System.Collections.Generic;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes strings representing user names from a <see cref="IReadOnlyList{T}" />.
    /// </summary>
    public sealed class UserNameRandomizer : IUserNameProvider {
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