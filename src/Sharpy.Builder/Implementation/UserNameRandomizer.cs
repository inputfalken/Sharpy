using System;
using System.Collections.Generic;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes strings representing user names from a <see cref="IReadOnlyList{T}" />.
    /// </summary>
    public sealed class UserNameRandomizer : IUserNameProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates <see cref="UserNameRandomizer" />.
        /// </summary>
        public UserNameRandomizer(Random random)
        {
            _random = random;
        }

        /// <summary>
        ///     Randomizes a user name.
        /// </summary>
        /// <returns>
        ///     A string representing a user name.
        /// </returns>
        public string UserName()
        {
            return _random.ListElement(Data.GetUserNames);
        }
    }
}