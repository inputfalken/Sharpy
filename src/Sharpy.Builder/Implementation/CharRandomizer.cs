using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public class CharRandomizer : ICharProvider
    {
        private readonly Random _random;

        public CharRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public char Char()
        {
            return Char(char.MinValue, char.MaxValue);
        }

        /// <inheritdoc />
        public char Char(char max)
        {
            return Char(char.MinValue, max);
        }

        /// <inheritdoc />
        public char Char(char min, char max)
        {
            return _random.Char(min, max);
        }
    }
}