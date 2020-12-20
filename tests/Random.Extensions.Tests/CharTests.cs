using System;
using System.Linq;
using NUnit.Framework;

namespace Random.Extensions.Tests
{
    [TestFixture]
    public class CharTests
    {
        private static readonly System.Random Random = new();

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new System.Random(i), x => x.Char('a', 'z'));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new System.Random(i), x => x.Char('a', 'z'));
        }


        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var chars = new char[Assertion.Amount];

            const char min = 'a';
            const char max = 'z';
            for (var i = 0; i < Assertion.Amount; i++)
                chars[i] = Random.Char(min, max);

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
                Random,
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
                Random,
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
                chars[i] = Random.Char(arg, arg);

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
                chars[i] = Random.Char(min, max);


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

            Assert.DoesNotThrow(() => Random.Char(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const char max = 'a';
            const char min = 'z';

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Char(min, max));
        }

        [Test]
        public void MinValue_And_Max_Does_Not_Throw()
        {
            Assert.DoesNotThrow(() => Random.Char(char.MinValue, char.MaxValue));
        }
    }
}