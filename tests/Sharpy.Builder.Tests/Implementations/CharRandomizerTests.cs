using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class CharRandomizerTests
    {
        private static readonly ICharProvider CharProvider = new CharRandomizer(new Random());

        [Test]
        public void No_Arg_Is_Deterministic_With_Seed()
        {
            var expected = new CharRandomizer(new Random(Assertion.MainSeed));
            var result = new CharRandomizer(new Random(Assertion.MainSeed));
            Assertion.AreEqual(expected, result, x => x.Char());
        }

        [Test]
        public void No_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new CharRandomizer(new Random(Assertion.MainSeed));
            var result = new CharRandomizer(new Random(Assertion.SecondarySeed));

            Assertion.AreNotEqual(expected, result, x => x.Char());
        }

        [Test]
        public void Max_Arg_Is_Deterministic_With_Seed()
        {
            var expected = new CharRandomizer(new Random(Assertion.MainSeed));
            var result = new CharRandomizer(new Random(Assertion.MainSeed));
            Assertion.AreEqual(expected, result, x => x.Char('z'));
        }

        [Test]
        public void Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new CharRandomizer(new Random(Assertion.MainSeed));
            var result = new CharRandomizer(new Random(Assertion.SecondarySeed));

            Assertion.AreNotEqual(expected, result, x => x.Char('z'));
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            var expected = new CharRandomizer(new Random(Assertion.MainSeed));
            var result = new CharRandomizer(new Random(Assertion.MainSeed));
            Assertion.AreEqual(expected, result, x => x.Char('a', 'z'));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new CharRandomizer(new Random(Assertion.MainSeed));
            var result = new CharRandomizer(new Random(Assertion.SecondarySeed));

            Assertion.AreNotEqual(expected, result, x => x.Char('a', 'z'));
        }

        [Test]
        public void No_Arg_Values_Are_Distributed()
        {
            var result = new CharRandomizer(new Random(Assertion.MainSeed));
            Assertion.IsDistributed(result, x => x.Char(), x => Assert.GreaterOrEqual(x.Count, char.MaxValue / 2));
        }

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var chars = new char[Assertion.Amount];

            for (var i = 0; i < Assertion.Amount; i++)
                chars[i] = CharProvider.Char();

            chars.AssertNotAllValuesAreTheSame();
            Assert.True(
                chars.All(x => x >= 0),
                "chars.All(x => x >= 0 && x < char.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var chars = new char[Assertion.Amount];

            const char max = 'z';
            for (var i = 0; i < Assertion.Amount; i++)
                chars[i] = CharProvider.Char(max);

            chars.AssertNotAllValuesAreTheSame();
            Assert.True(
                chars.All(x => x >= 0 && x <= max),
                "chars.All(x => x >= 0 && x <= max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var chars = new char[Assertion.Amount];

            const char min = 'a';
            const char max = 'z';
            for (var i = 0; i < Assertion.Amount; i++)
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

            Assertion.IsDistributed(
                CharProvider,
                x => x.Char(min, max),
                x =>
                {
                    Assert.AreEqual(
                        digits.Length,
                        x.Join(digits, y => y.Key, y => y, (y, z) => true).Count()
                    );
                }
            );
        }

        [Test]
        public void All_Chars_Are_Distributed_A_To_Z()
        {
            const char min = 'a';
            const char max = 'z';

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

            Assertion.IsDistributed(
                CharProvider,
                x => x.Char(min, max),
                x =>
                {
                    Assert.AreEqual(
                        alphabet.Length,
                        x.Join(alphabet, y => y.Key, y => y, (y, z) => true).Count()
                    );
                }
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var chars = new char[Assertion.Amount];

            const char arg = 'h';
            for (var i = 0; i < Assertion.Amount; i++)
                chars[i] = CharProvider.Char(arg, arg);

            Assert.True(
                chars.All(x => x == arg),
                "chars.All(x => x == arg)"
            );
        }

        [Test]
        public void Inclusive_Max_Arg()
        {
            var chars = new char[Assertion.Amount];

            const char max = char.MaxValue;
            const char min = max;
            for (var i = 0; i < Assertion.Amount; i++)
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