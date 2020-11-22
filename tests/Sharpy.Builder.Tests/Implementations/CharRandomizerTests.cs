using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class CharRandomizerTests
    {
        private const int Amount = 100000;
        private const int Repeats = 100;

        private static readonly ICharProvider CharProvider = new CharRandomizer(new Random());

        [Test, Repeat(Repeats)]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var chars = new char[Amount];

            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char();

            chars.AssertNotAllValuesAreTheSame();
            Assert.True(
                chars.All(x => x >= 0),
                "chars.All(x => x >= 0 && x < char.MaxValue)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var chars = new char[Amount];

            const char max = 'z';
            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char(max);

            chars.AssertNotAllValuesAreTheSame();
            Assert.True(
                chars.All(x => x >= 0 && x <= max),
                "chars.All(x => x >= 0 && x <= max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var chars = new char[Amount];

            const char min = 'a';
            const char max = 'z';
            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char(min, max);

            chars.AssertNotAllValuesAreTheSame();
            Assert.True(
                chars.All(x => x >= min && x <= max),
                "chars.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void All_Chars_Are_Distributed_0_To_9()
        {
            const char min = '0';
            const char max = '9';
            var chars = new char[Amount];
            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char(min, max);

            var digits = new[]
            {
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9'
            };

            Assert.True(digits
                    .SelectMany(x => chars)
                    .GroupBy(x => x)
                    .Count() == digits.Length,
                "digits.SelectMany(x => chars).GroupBy(x => x).Count() == digits.Length"
            );
        }

        [Test]
        public void All_Chars_Are_Distributed_A_To_Z()
        {
            const char min = 'a';
            const char max = 'z';
            var chars = new char[Amount];
            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char(min, max);

            var alphabet = new[]
            {
                'a',
                'b',
                'c',
                'd',
                'e',
                'f',
                'g',
                'h',
                'i',
                'j',
                'k',
                'l',
                'm',
                'n',
                'o',
                'p',
                'q',
                'r',
                's',
                't',
                'u',
                'v',
                'w',
                'x',
                'y',
                'z'
            };

            Assert.True(
                alphabet
                    .SelectMany(x => chars)
                    .GroupBy(x => x)
                    .Count() == alphabet.Length,
                "Alphabet.SelectMany(x => chars).GroupBy(x => x).Count() == Alphabet.Length"
            );
        }

        [Test, Repeat(Repeats)]
        public void Inclusive_Min_Arg()
        {
            var chars = new char[Amount];

            const char arg = 'h';
            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char(arg, arg);

            Assert.True(
                chars.All(x => x == arg),
                "chars.All(x => x == arg)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Inclusive_Max_Arg()
        {
            var chars = new char[Amount];

            const char max = char.MaxValue;
            const char min = max;
            for (var i = 0; i < Amount; i++)
                chars[i] = CharProvider.Char(min, max);


            Assert.True(
                chars.All(x => x == min),
                "chars.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const char max = 'h';
            const char min = max;

            Assert.DoesNotThrow(() => CharProvider.Char(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const char max = 'a';
            const char min = 'z';

            Assert.Throws<ArgumentOutOfRangeException>(() => CharProvider.Char(min, max));
        }
    }
}