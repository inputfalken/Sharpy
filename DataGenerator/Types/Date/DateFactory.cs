using System;

namespace DataGenerator.Types.Date {
    internal static class DateFactory {
        public static DateTime BirthDate(int age) {
            return new DateTime(DateTime.UtcNow.Year - age, 10, 5);
        }
    }
}