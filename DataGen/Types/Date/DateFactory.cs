﻿using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using static NodaTime.Period;

namespace DataGen.Types.Date {
    public static class DateFactory {
        public static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;


        /// <summary>
        ///     Will create a sequence of the values given from current date
        /// </summary>
        /// <param name="length">Length of sequence</param>
        /// <param name="sequenceDate">Date to be sequenced</param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static IEnumerable<LocalDate> CreateSequence(int length, LocalDate sequenceDate,
            LocalDate startDate = default(LocalDate)) {
            var localDate = startDate == default(LocalDate) ? CurrentLocalDate : startDate;
            return Enumerable.Range(1, length)
                .Select(i => localDate
                    .PlusYears(sequenceDate.Year * i)
                    .PlusMonths(sequenceDate.Month * i)
                    .PlusDays(sequenceDate.Day * i));
        }


        /// <summary>
        ///     Will add or subtract the date argument from current date.
        /// <param name="date">The date to subtract/add to/from current date</param>
        /// <param name="subtract">if set to false it will add the values to the current date rather than subtracting</param>
        /// </summary>
        public static LocalDate CreateDate(LocalDate date, bool subtract = true) => subtract
            ? CurrentLocalDate.Minus(FromYears(date.Year)).Minus(FromMonths(date.Month)).Minus(FromDays(date.Day))
            : CurrentLocalDate.Plus(FromYears(date.Year)).Plus(FromMonths(date.Month)).Plus(FromDays(date.Day));

        /// <summary>
        ///     This overload will do the same but with ints
        /// <param name="subtract">if set to false it will add the values to the current date rather than subtracting</param>
        /// </summary>
        public static LocalDate CreateDate(int year, int month, int day, bool subtract = true) {
            if (year < 0 || month < 0 || day < 0)
                throw new ArgumentException("Year/Month/Day cannot be negative");
            return subtract
                ? CurrentLocalDate.Minus(FromYears(year)).Minus(FromMonths(month)).Minus(FromDays(day))
                : CurrentLocalDate.Plus(FromYears(year)).Plus(FromMonths(month)).Plus(FromDays(day));
        }
    }
}