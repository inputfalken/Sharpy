using System;
using System.Collections.Generic;

namespace Sharpy.Builder.Implementation.ExtensionMethods
{
    internal static class RandomExtensions
    {
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

            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max.");

            if (min == max)
                return min;

            return NextDecimal(random) * (max - min) + min;
            //return max * nextDecimalSample + min * (1 - nextDecimalSample);
        }

        public static double Double(this Random random, double min, double max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max.");

            if (min == max)
                return min;

            return random.NextDouble() * (max - min) + min;
        }

        public static bool Bool(this Random random)
        {
            return random.Next(2) != 0;
        }

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

        public static T Argument<T>(this Random random, T first, T second, params T[] additional)
        {
            return random.Next(-2, additional.Length) switch
            {
                -2 => first,
                -1 => second,
                {} x => additional[x]
            };
        }

        public static long Long(this Random random, long min, long max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max.");

            if (min == max)
                return max;

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

        public static TimeSpan TimeSpan(this Random random, TimeSpan min, TimeSpan max)
        {
            if (min > max)
                throw new ArgumentException($"Parameter can not be greater than max'{nameof(min)}'.");

            return System.TimeSpan.FromMilliseconds(
                random.Double(min.TotalMilliseconds, max.TotalMilliseconds)
            );
        }

        public static int Day(this Random random, int year, int month)
        {
            return random.Next(1, System.DateTime.DaysInMonth(year, month) + 1);
        }

        public static int Month(this Random random)
        {
            return random.Next(1, 13);
        }

        public static int Hour(this Random random)
        {
            return random.Next(0, 24);
        }

        public static int Minute(this Random random)
        {
            return random.Next(0, 60);
        }

        public static int Second(this Random random)
        {
            return random.Next(0, 60);
        }

        public static int MilliSecond(this Random random)
        {
            return random.Next(0, 1000);
        }

        public static DateTime DateTime(this Random random, DateTime min, DateTime max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "Can not be greater than max.");

            if (min == max)
                return min;

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
    }
}