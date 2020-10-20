using System;
using Sharpy.Builder.IProviders;
using static System.DateTime;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes <see cref="DateTime" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class DateRandomizer : IDateProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates <see cref="DateRandomizer" />.
        /// </summary>
        /// <param name="random"></param>
        public DateRandomizer(Random random) => _random = random;

        /// <inheritdoc cref="IDateProvider.DateByAge(int)"/>
        public DateTime DateByAge(int age)
        {
            if (age < 0)
                throw new ArgumentException($"{nameof(age)} cannot be negative");

            var date = Now
                .AddYears(-age)
                .AddMonths(-_random.Next(1, 12) + 1);

            return date
                .AddDays(-RandomizeDay(date.Year, date.Month))
                .AddHours(RandomizeHour())
                .AddMinutes(RandomizeMinute())
                .AddSeconds(RandomizeSecond())
                .AddMilliseconds(RandomizeMilliSecond());
        }

        /// <inheritdoc cref="IDateProvider.DateByYear(int)"/>
        public DateTime DateByYear(int year)
        {
            if (year <= MinValue.Year) throw new ArgumentException($"{nameof(year)} cannot be negative");
            var month = RandomizeMonth();
            return new DateTime(
                year,
                month,
                RandomizeDay(year, month),
                RandomizeHour(),
                RandomizeMinute(),
                RandomizeSecond(),
                RandomizeMilliSecond()
            );
        }

        private int RandomizeDay(int year, int month) => _random.Next(1, DaysInMonth(year, month) + 1);
        private int RandomizeMonth() => _random.Next(1, 13);
        private int RandomizeHour() => _random.Next(0, 24);
        private int RandomizeMinute() => _random.Next(0, 60);
        private int RandomizeSecond() => _random.Next(0, 60);
        private int RandomizeMilliSecond() => _random.Next(0, 1000);

        public DateTime Date() => Date(MinValue, MaxValue);

        /// <inheritdoc cref="IDateProvider.Date(DateTime)"/>
        public DateTime Date(DateTime max)
        {
            return Date(MinValue, max);
        }

        /// <inheritdoc cref="IDateProvider.Date(DateTime, DateTime)"/>
        public DateTime Date(DateTime min, DateTime max)
        {
            var isSameYear = min.Year == max.Year;
            var year = isSameYear ? min.Year : _random.Next(min.Year, max.Year);

            var isSameMonth = min.Month == max.Month;
            var month = isSameYear
                ? isSameMonth
                    ? min.Month
                    : _random.Next(1, max.Month)
                : RandomizeMonth();

            var isSameDay = min.Day == max.Day;
            var day = isSameYear && isSameMonth
                ? isSameDay
                    ? min.Day
                    : _random.Next(1, max.Day)
                : RandomizeDay(year, month);

            var isSameHour = min.Hour == max.Hour;
            var hour = isSameYear && isSameMonth && isSameDay
                ? isSameHour
                    ? min.Hour
                    : _random.Next(1, max.Hour)
                : RandomizeHour();

            var isSameMinute = min.Minute == max.Minute;
            var minute = isSameYear && isSameMonth && isSameDay && isSameHour
                ? isSameMinute
                    ? min.Minute
                    : _random.Next(1, max.Minute)
                : RandomizeMinute();

            var isSameSecond = min.Second == max.Second;

            var second = isSameYear && isSameMonth && isSameDay && isSameHour
                ? isSameSecond
                    ? min.Second
                    : _random.Next(1, max.Second)
                : RandomizeSecond();


            var isSameMillisSecond = min.Millisecond == max.Millisecond;

            var milliSecond = isSameYear && isSameMonth && isSameDay && isSameHour && isSameMinute && isSameSecond
                ? isSameMillisSecond
                    ? min.Millisecond
                    : _random.Next(1, max.Millisecond)
                : RandomizeMilliSecond();


            return new DateTime(year, month, day, hour, minute, second, milliSecond);
        }
    }
}