using System;

namespace DataGenerator.Types.Date {
    internal static class DateFactory {
      //In here create a range of methods doing anything from generating a birth date to generate a date time within a specifik range from arguments....
        public static DateTime BirthDate(int age) {
            return new DateTime(DateTime.UtcNow.Year - age, 10, 5);
        }
    }
}
