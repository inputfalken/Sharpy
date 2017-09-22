using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    /// Randomizes user names a <see cref="IReadOnlyList{T}"/> implementation.
    /// </summary>
    public class UserNameRandomizer : IUserNameProvider {
        private readonly IReadOnlyList<string> _usernames;
        private readonly Random _random;

        /// <summary>
        /// Randomizes user names from the argument <paramref name="usernames"/> by using argument <paramref name="random"/>.
        /// </summary>
        /// <param name="usernames">
        /// The <see cref="IReadOnlyList{T}"/> of user names.
        /// </param>
        /// <param name="random">
        /// Used for randomizing.
        /// </param>
        public UserNameRandomizer(IReadOnlyList<string> usernames, Random random) {
            _usernames = usernames;
            _random = random;
        }

        /// <summary>
        /// Randomizes a user name.
        /// </summary>
        /// <returns>
        /// A string representing a user name.
        /// </returns>
        public string UserName() => _usernames.RandomItem(_random);
    }
}