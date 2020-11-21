using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    public class CharRandomizer : ICharProvider
    {
        private readonly Random _random;

        public CharRandomizer(Random random)
        {
            _random = random;
        }

        public char Char()
        {
            return Char(char.MinValue, char.MaxValue);
        }

        public char Char(char max)
        {
            return Char(char.MinValue, max);
        }

        public char Char(char min, char max)
        {
            return _random.Char(min, max);
        }
    }
}