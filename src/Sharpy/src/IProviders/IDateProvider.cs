using System;

namespace Sharpy.IProviders {
    public interface IDateProvider {
        DateTime DateByAge(int age);
        DateTime DateByYear(int year);
    }
}