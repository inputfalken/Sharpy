using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using Sharpy.Properties;

namespace Sharpy.Implementation {
    internal class UserNameRandomizer : IStringProvider {
        private readonly Random _random;

        public UserNameRandomizer(Random random) {
            _random = random;
        }

        private Lazy<string[]> LazyUsernames { get; } =
            new Lazy<string[]>(() => Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None));


        private IEnumerable<string> UserNames => LazyUsernames.Value;

        public string String() => LazyUsernames.Value.RandomItem(_random);
    }
}