using System;
using System.Collections.Generic;

namespace Random.Extensions
{
    /// <summary>
    ///     Contains a set of static extension method on System.Random.
    /// </summary>
    internal static class Random
    {
        /// <summary>
        ///     Randomizes a System.Int32 within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.Int32 within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static int Int(this System.Random random, in int min, in int max)
        {
            return random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => random.Next(min, max)
            };
        }

        /// <summary>
        ///     Randomizes a System.Decimal within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.Decimal within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static decimal Decimal(
            this System.Random random,
            in decimal min,
            in decimal max
        )
        {
            static decimal NextDecimal(System.Random random)
            {
                static int NextInt32(System.Random random)
                {
                    var firstBits = random.Next(0, 1 << 4) << 28;
                    var lastBits = random.Next(0, 1 << 28);
                    return firstBits | lastBits;
                }

                var sample = 1m;
                //After ~200 million tries this never took more than one attempt but it is possible to generate combinations of a, b, and c with the approach below resulting in a sample >= 1.
                while (sample >= 1)
                {
                    var a = NextInt32(random);
                    var b = NextInt32(random);
                    //The high bits of 0.9999999999999999999999999999m are 542101086.
                    var c = random.Next(542101087);
                    sample = new decimal(a, b, c, false, 28);
                }

                return sample;
            }

            return random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => NextDecimal(random) * (max - min) + min
            };
        }

        /// <summary>
        ///     Randomizes a System.Single within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.Single within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static float Float(
            this System.Random random,
            in float min,
            in float max
        )
        {
            var res = random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => (float) (random.NextDouble() * (max - min) + min)
            };

            return res == max ? min : res;
        }

        /// <summary>
        ///     Randomizes a System.Double within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.Double within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static double Double(
            this System.Random random,
            in double min,
            in double max
        )
        {
            var res = random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => random.NextDouble() * (max - min) + min
            };

            return res == max ? min : res;
        }

        /// <summary>
        ///     Randomizes a System.Boolean.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <returns>
        ///     A randomized System.Boolean.
        /// </returns>
        public static bool Bool(this System.Random random)
        {
            return random.Next(2) != 0;
        }

        /// <summary>
        ///     Returns a random element from the System.Collections.Generic.IReadonlyList&lt;out T&gt;.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="list">
        ///     The list to randomize from.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the elements of <paramref name="list" />.
        /// </typeparam>
        /// <exception cref="ArgumentException">
        ///     When <paramref name="list" /> is empty.
        /// </exception>
        /// <returns>
        ///     A randomized <typeparamref name="T" /> from the <paramref name="list" />.
        /// </returns>
        public static T ListElement<T>(
            this System.Random random,
            in IReadOnlyList<T> list
        )
        {
            return (random, list) switch
            {
                {Item2: {Count: 0}} => throw new ArgumentException("List can not be empty.", nameof(list)),
                {Item2: {Count: 1} x} => x[0],
                _ => list[random.Next(list.Count)]
            };
        }

        /// <summary>
        ///     Returns a random element from the System.ReadOnlySpan&lt;T&gt;.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="span">
        ///     The span to randomize from.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the elements of <paramref name="span" />.
        /// </typeparam>
        /// <exception cref="ArgumentException">
        ///     When <paramref name="span" /> is empty.
        /// </exception>
        /// <returns>
        ///     A randomized <typeparamref name="T" /> from the <paramref name="span" />.
        /// </returns>
        public static T SpanElement<T>(
            this System.Random random,
            in ReadOnlySpan<T> span
        )
        {
            return random switch
            {
                _ when span.Length == 0 => throw new ArgumentException("Span can not be empty.", nameof(span)),
                _ when span.Length == 1 => span[0],
                _ => span[random.Next(span.Length)]
            };
        }


        /// <summary>
        ///     Returns a random element from the System.Span&lt;T&gt;.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="span">
        ///     The span to randomize from.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the elements of <paramref name="span" />.
        /// </typeparam>
        /// <exception cref="ArgumentException">
        ///     When <paramref name="span" /> is empty.
        /// </exception>
        /// <returns>
        ///     A randomized <typeparamref name="T" /> from the <paramref name="span" />.
        /// </returns>
        public static T SpanElement<T>(
            this System.Random random,
            in Span<T> span
        )
        {
            return random switch
            {
                _ when span.Length == 0 => throw new ArgumentException("Span can not be empty.", nameof(span)),
                _ when span.Length == 1 => span[0],
                _ => span[random.Next(span.Length)]
            };
        }

        /// <summary>
        ///     Returns a randomized argument.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <typeparam name="T">
        ///     The type provided in the arguments.
        /// </typeparam>
        /// <returns>
        ///     A randomized argument.
        /// </returns>
        public static T Argument<T>(
            this System.Random random,
            in T first,
            in T second
        )
        {
            return random switch
            {
                _ => random.Next(0, 2) switch
                {
                    1 => second,
                    0 => first,
                    _ => throw new IndexOutOfRangeException()
                }
            };
        }

        /// <summary>
        ///     Returns a randomized argument.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <param name="third">
        ///     The third argument.
        /// </param>
        /// <typeparam name="T">
        ///     The type provided in the arguments.
        /// </typeparam>
        /// <returns>
        ///     A randomized argument.
        /// </returns>
        public static T Argument<T>(
            this System.Random random,
            in T first,
            in T second,
            in T third
        )
        {
            return random switch
            {
                _ => random.Next(0, 3) switch
                {
                    2 => third,
                    1 => second,
                    0 => first,
                    _ => throw new IndexOutOfRangeException()
                }
            };
        }

        /// <summary>
        ///     Returns a randomized argument.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <param name="third">
        ///     The third argument.
        /// </param>
        /// <param name="fourth">
        ///     The fourth argument.
        /// </param>
        /// <typeparam name="T">
        ///     The type provided in the arguments.
        /// </typeparam>
        /// <returns>
        ///     A randomized argument.
        /// </returns>
        public static T Argument<T>(
            this System.Random random,
            in T first,
            in T second,
            in T third,
            in T fourth
        )
        {
            return random switch
            {
                _ => random.Next(0, 4) switch
                {
                    3 => fourth,
                    2 => third,
                    1 => second,
                    0 => first,
                    _ => throw new IndexOutOfRangeException()
                }
            };
        }

        /// <summary>
        ///     Returns a randomized argument.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <param name="third">
        ///     The third argument.
        /// </param>
        /// <param name="fourth">
        ///     The fourth argument.
        /// </param>
        /// <param name="fifth">
        ///     The fourth argument.
        /// </param>
        /// <typeparam name="T">
        ///     The type provided in the arguments.
        /// </typeparam>
        /// <returns>
        ///     A randomized argument.
        /// </returns>
        public static T Argument<T>(
            this System.Random random,
            in T first,
            in T second,
            in T third,
            in T fourth,
            in T fifth
        )
        {
            return random switch
            {
                _ => random.Next(0, 5) switch
                {
                    4 => fifth,
                    3 => fourth,
                    2 => third,
                    1 => second,
                    0 => first,
                    _ => throw new IndexOutOfRangeException()
                }
            };
        }

        /// <summary>
        ///     Returns a randomized argument.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <param name="third">
        ///     The third argument.
        /// </param>
        /// <param name="fourth">
        ///     The fourth argument.
        /// </param>
        /// <param name="fifth">
        ///     The fourth argument.
        /// </param>
        /// <param name="additional">
        ///     The remaining arguments.
        /// </param>
        /// <typeparam name="T">
        ///     The type provided in the arguments.
        /// </typeparam>
        /// <returns>
        ///     A randomized argument.
        /// </returns>
        public static T Argument<T>(
            this System.Random random,
            in T first,
            in T second,
            in T third,
            in T fourth,
            in T fifth,
            params T[] additional
        )
        {
            return random switch
            {
                _ => random.Next(-5, additional.Length) switch
                {
                    -5 => fifth,
                    -4 => fourth,
                    -3 => third,
                    -2 => second,
                    -1 => first,
                    { } x => additional[x]
                }
            };
        }

        /// <summary>
        ///     Randomizes a System.Long within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.Long within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static long Long(
            this System.Random random,
            in long min,
            in long max
        )
        {
            static long NextLong(System.Random random, long min, long max)
            {
                //Working with ulong so that modulo works correctly with values > long.MaxValue
                var uRange = (ulong) (max - min);

                //Prevent a modulo bias; see http://stackoverflow.com/a/10984975/238419
                //for more information.
                //In the worst case, the expected number of calls is 2 (though usually it's
                //much closer to 1) so this loop doesn't really hurt performance at all.
                ulong ulongRand;
                do
                {
                    var buf = new byte[8];
                    random.NextBytes(buf);
                    ulongRand = (ulong) BitConverter.ToInt64(buf, 0);
                } while (ulongRand > ulong.MaxValue - (ulong.MaxValue % uRange + 1) % uRange);

                return (long) (ulongRand % uRange) + min;
            }

            return random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => NextLong(random, min, max)
            };
        }

        /// <summary>
        ///     Randomizes a System.TimeSpan within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.TimeSpan within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static TimeSpan TimeSpan(
            this System.Random random,
            in TimeSpan min,
            in TimeSpan max
        )
        {
            return random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => System.TimeSpan.FromMilliseconds(
                        random.Double(min.TotalMilliseconds, max.TotalMilliseconds)
                    ) switch
                    {
                        { } x when x == max => x.Subtract(System.TimeSpan.FromTicks(1)),
                        { } x => x
                    }
            };
        }

        /// <summary>
        ///     Randomizes a System.DateTime within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.DateTime within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static DateTime DateTime(
            this System.Random random,
            in DateTime min,
            in DateTime max
        )
        {
            return random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => new DateTime(random.Long(min.Ticks, max.Ticks))
            };
        }

        /// <summary>
        ///     Randomizes a System.DateTimeOffset within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.DateTimeOffset within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static DateTimeOffset DateTimeOffset(
            this System.Random random,
            in DateTimeOffset min,
            in DateTimeOffset max
        )
        {
            var dateTime = DateTime(random, min.DateTime, max.DateTime);
            var offset = System.DateTimeOffset.Now.Offset;

            return dateTime.TimeOfDay < offset
                ? dateTime.Add(offset)
                : dateTime;
        }

        /// <summary>
        ///     Randomizes a System.Char within <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="random">
        ///     The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///     The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///     The maximum inclusive value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     When <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        /// <returns>
        ///     A randomized System.Char within <paramref name="min" /> and <paramref name="max" />.
        /// </returns>
        public static char Char(
            this System.Random random,
            in char min,
            in char max
        )
        {
            return random switch
            {
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min),
                    $"Can not be greater than {nameof(max)}."),
                _ when min == max => min,
                _ => (char) random.Next(min, max + 1)
            };
        }
    }
}