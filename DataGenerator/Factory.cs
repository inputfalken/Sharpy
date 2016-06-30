using System;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Factory
    {
        public static string GetLastName() => new LastName().ToString();

        public static string GetFirstName(Gender gender) => new FirstName(gender).ToString();
    }
}