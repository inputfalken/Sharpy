using System;
using System.Collections.Generic;

namespace Sharpy.Builder.Implementation.ExtensionMethods
{
    /// <summary>
    ///   Contains a set of static extension method on System.Random.
    /// </summary>
    internal static class RandomExtensions
    {
        /// <summary>
        ///   Randomizes a System.Decimal within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.Decimal within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static decimal Decimal(this Random random, decimal min, decimal max)
        {
            static decimal NextDecimal(Random random)
            {
                static int NextInt32(Random random)
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
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ when min == max => min,
                _ => NextDecimal(random) * (max - min) + min
            };
        }

        /// <summary>
        ///   Randomizes a System.Single within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.Single within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static float Float(this Random random, float min, float max)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ when min == max => min,
                _ => (float) (random.NextDouble() * (max - min) + min) switch
                {
                    {} x when x == max => x - 000_001f,
                    {} x when x < 0 => Math.Abs(x),
                    {} x => x
                }
            };
        }

        /// <summary>
        ///   Randomizes a System.Double within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.Double within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static double Double(this Random random, double min, double max)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ when min == max => min,
                _ => random.NextDouble() * (max - min) + min
            };
        }

        /// <summary>
        ///   Randomizes a System.Boolean.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized System.Boolean.
        /// </returns>
        public static bool Bool(this Random random)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(2) != 0
            };
        }

        /// <summary>
        ///   Returns a random element from the System.Collections.Generic.IReadonlyList&lt;out T&gt;.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="list">
        ///   The list to randomize from.
        /// </param>
        /// <typeparam name="T">
        ///   The type of the elements of <paramref name="list"/>.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> or <paramref name="list"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   When <paramref name="list"/> is empty.
        /// </exception>
        /// <returns>
        ///   A randomized <typeparamref name="T"/> from the <paramref name="list"/>.
        /// </returns>
        public static T ListElement<T>(this Random random, IReadOnlyList<T> list)
        {
            return (random, list) switch
            {
                {random : null} => throw new ArgumentNullException(nameof(random)),
                {list: null} => throw new ArgumentNullException(nameof(list)),
                {list: {Count: 0}} => throw new ArgumentException("List can not be empty.", nameof(list)),
                {list: {Count: 1} x} => x[0],
                _ => list[random.Next(list.Count)]
            };
        }

        /// <summary>
        ///   Returns a random element from the System.ReadOnlySpan&lt;T&gt;.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="span">
        ///   The span to randomize from.
        /// </param>
        /// <typeparam name="T">
        ///   The type of the elements of <paramref name="span"/>.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   When <paramref name="span"/> is empty.
        /// </exception>
        /// <returns>
        ///   A randomized <typeparamref name="T"/> from the <paramref name="span"/>.
        /// </returns>
        public static T SpanElement<T>(this Random random, ReadOnlySpan<T> span)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when span.Length == 0 => throw new ArgumentException("Span can not be empty.", nameof(span)),
                _ when span.Length == 1 => span[0],
                _ => span[random.Next(span.Length)]
            };
        }


        /// <summary>
        ///   Returns a random element from the System.Span&lt;T&gt;.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="span">
        ///   The span to randomize from.
        /// </param>
        /// <typeparam name="T">
        ///   The type of the elements of <paramref name="span"/>.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   When <paramref name="span"/> is empty.
        /// </exception>
        /// <returns>
        ///   A randomized <typeparamref name="T"/> from the <paramref name="span"/>.
        /// </returns>
        public static T SpanElement<T>(this Random random, Span<T> span)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when span.Length == 0 => throw new ArgumentException("Span can not be empty.", nameof(span)),
                _ when span.Length == 1 => span[0],
                _ => span[random.Next(span.Length)]
            };
        }

        /// <summary>
        ///   Returns a randomized argument.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="first">
        ///   The first argument.
        /// </param>
        /// <param name="second">
        ///   The second argument.
        /// </param>
        /// <param name="additional">
        ///   The optional remaining arguments.
        /// </param>
        /// <typeparam name="T">
        ///   The type provided in the arguments.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized argument.
        /// </returns>
        public static T Argument<T>(this Random random, T first, T second, params T[] additional)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(-2, additional.Length) switch
                {
                    -2 => first,
                    -1 => second,
                    {} x => additional[x]
                }
            };
        }

        /// <summary>
        ///   Randomizes a System.Long within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.Long within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static long Long(this Random random, long min, long max)
        {
            static long NextLong(Random random, long min, long max)
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
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ when min == max => min,
                _ => NextLong(random, min, max)
            };
        }

        /// <summary>
        ///   Randomizes a System.TimeSpan within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.TimeSpan within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static TimeSpan TimeSpan(this Random random, TimeSpan min, TimeSpan max)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ when min == max => min,
                _ => System.TimeSpan.FromMilliseconds(
                        random.Double(min.TotalMilliseconds, max.TotalMilliseconds)
                    ) switch
                    {
                        {} x when x == max => x.Subtract(System.TimeSpan.FromTicks(1)),
                        {} x => x
                    }
            };
        }

        /// <summary>
        ///  Randomizes a day relative to the <paramref name="year"/> and  <paramref name="month"/>..
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="year">
        ///   The year.
        /// </param>
        /// <param name="month">
        ///   The month relative to <paramref name="year"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A valid day relative to the <paramref name="year"/> and  <paramref name="month"/>.
        /// </returns>
        public static int Day(this Random random, int year, int month)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(1, System.DateTime.DaysInMonth(year, month) + 1)
            };
        }

        /// <summary>
        ///  Randomizes a month.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized month.
        /// </returns>
        public static int Month(this Random random)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(1, 13)
            };
        }

        /// <summary>
        ///  Randomizes an hour.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized hour.
        /// </returns>
        public static int Hour(this Random random)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(0, 24)
            };
        }

        /// <summary>
        ///  Randomizes a minute.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized minute.
        /// </returns>
        public static int Minute(this Random random)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(0, 60)
            };
        }

        /// <summary>
        ///  Randomizes a second.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized second.
        /// </returns>
        public static int Second(this Random random)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(0, 60)
            };
        }

        /// <summary>
        ///  Randomizes a millisecond.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <returns>
        ///   A randomized millisecond.
        /// </returns>
        public static int MilliSecond(this Random random)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ => random.Next(0, 1000)
            };
        }

        /// <summary>
        ///   Randomizes a System.DateTime within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.DateTime within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static DateTime DateTime(this Random random, DateTime min, DateTime max)
        {
            static DateTime NextDateTime(Random random, DateTime min, DateTime max)
            {
                var isSameYear = min.Year == max.Year;
                var year = isSameYear ? min.Year : random.Next(min.Year, max.Year);

                var isSameMonth = min.Month == max.Month;
                var month = isSameYear
                    ? isSameMonth
                        ? min.Month
                        : random.Next(1, max.Month)
                    : Month(random);

                var isSameDay = min.Day == max.Day;
                var day = isSameYear && isSameMonth
                    ? isSameDay
                        ? min.Day
                        : random.Next(1, max.Day)
                    : Day(random, year, month);

                var isSameHour = min.Hour == max.Hour;
                var hour = isSameYear && isSameMonth && isSameDay
                    ? isSameHour
                        ? min.Hour
                        : random.Next(1, max.Hour)
                    : Hour(random);

                var isSameMinute = min.Minute == max.Minute;
                var minute = isSameYear && isSameMonth && isSameDay && isSameHour
                    ? isSameMinute
                        ? min.Minute
                        : random.Next(1, max.Minute)
                    : Minute(random);

                var isSameSecond = min.Second == max.Second;

                var second = isSameYear && isSameMonth && isSameDay && isSameHour
                    ? isSameSecond
                        ? min.Second
                        : random.Next(1, max.Second)
                    : Second(random);


                var isSameMillisSecond = min.Millisecond == max.Millisecond;

                var milliSecond = isSameYear && isSameMonth && isSameDay && isSameHour && isSameMinute && isSameSecond
                    ? isSameMillisSecond
                        ? min.Millisecond
                        : random.Next(1, max.Millisecond)
                    : MilliSecond(random);

                return new DateTime(year, month, day, hour, minute, second, milliSecond);
            }

            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ when min == max => min,
                _ => NextDateTime(random, min, max)
            };
        }

        /// <summary>
        ///   Randomizes a System.DateTimeOffset within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum exclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.DateTimeOffset within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static DateTimeOffset DateTimeOffset(this Random random, DateTimeOffset min, DateTimeOffset max)
        {
            return DateTime(random, min.DateTime, max.DateTime);
        }

        /// <summary>
        ///   Randomizes a System.Char within <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">
        ///   The System.Random to randomize with.
        /// </param>
        /// <param name="min">
        ///   The minimum inclusive value.
        /// </param>
        /// <param name="max">
        ///   The maximum inclusive value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   When <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   When <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        /// <returns>
        ///   A randomized System.Char within <paramref name="min"/> and <paramref name="max"/>.
        /// </returns>
        public static char Char(this Random random, char min, char max)
        {
            return random switch
            {
                null => throw new ArgumentNullException(nameof(random)),
                _ when min > max => throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max."),
                _ => (char) random.Next(min, max + 1)
            };
        }
    }
}