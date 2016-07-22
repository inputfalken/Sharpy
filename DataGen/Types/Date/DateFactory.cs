using NodaTime;
using static NodaTime.Period;

namespace DataGen.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;

        /// <summary>
        ///     Will add To current date
        /// </summary>
        /// <param name="years"></param>
        /// <param name="month"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static LocalDate Addition(uint years = 0, uint month = 0, uint days = 0)
            => CurrentLocalDate
                .Plus(FromYears(years))
                .Plus(FromMonths(month))
                .Plus(FromDays(days));

        /// <summary>
        ///     Will subtract from current date
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static LocalDate Subtraction(uint years = 0, uint months = 0, uint days = 0)
            => CurrentLocalDate
                .Minus(FromYears(years))
                .Minus(FromMonths(months))
                .Minus(FromDays(days));
    }
}