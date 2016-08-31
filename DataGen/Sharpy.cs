using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.Mail;
using static DataGen.DataCollections;

namespace DataGen {
    public static class Sharpy {
        ///<summary>
        ///     This is the field which gets used if you use the method which do not ask for a fetcher
        /// </summary>
        private static readonly Randomizer DefaultRandomizer = new Randomizer(Names.Value, UserNames.Value,
            new MailGenerator("gmail.com", "hotmail.com", "yahoo.com"), CountryCodes.Value.RandomItem);

        ///<summary>
        ///     Returns a Generator which you can use to create one instance or a collection of type given
        ///     For a type with a consturctor you can just new up an instance of the type and satisfy the constructor args.
        ///     if you don't have a constructor you can new up an instance and assign it's props/fields
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public static Generator<T> CreateGenerator<T>(Func<Randomizer, T> func)
            => new Generator<T>(() => func(DefaultRandomizer));

        ///<summary>
        ///     Returns a Generator which you can use to create one instance or a collection of type given
        ///     For a type with a consturctor you can just new up an instance of the type and satisfy the constructor args.
        ///     if you don't have a constructor you can new up an instance and assign it's props/fields
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        ///     You need to specify a new fetcher for this overload the data used for the fetcher is found in the DataCollections class
        /// </summary>
        public static Generator<T> CreateGenerator<T>(Func<Randomizer, T> func, Randomizer randomizer)
            => new Generator<T>(() => func(randomizer));

        ///<summary>
        ///     Gives a new instance of the type used
        ///     You need to specify a new fetcher for this overload the data used for the fetcher is found in the DataCollections class
        /// </summary>
        public static T Generate<T>(Func<Randomizer, T> func, Randomizer randomizer) => func(randomizer);

        ///<summary>
        ///     Gives a new instance of the type used
        /// </summary>
        public static T Generate<T>(Func<Randomizer, T> func) => func(DefaultRandomizer);
    }

    public class Generator<T> {
        private readonly Func<T> _func;

        public Generator(Func<T> func) {
            _func = func;
        }

        public T Generate() => _func();

        public IEnumerable<T> Generate(int ammount) => Enumerable.Range(0, ammount).Select(i => _func());
    }
}