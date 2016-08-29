using System;
using System.Linq;
using DataGen.Types.Mail;
using static DataGen.DataCollections;

namespace DataGen {
    public static class Sharpy {
        ///<summary>
        ///     This is the field which gets used if you use the method which do not ask for a fetcher
        /// </summary>
        private static readonly Fetcher DefaultFetcher = new Fetcher(Names.Value, UserNames.Value,
            new MailGenerator(new []{"gmail.com", "hotmail.com", "yahoo.com"}), CountryCodes.Value.RandomItem);

        ///<summary>
        ///     Gives a method which can be used to creating random instances of the type given
        ///     All you have to do is to use the type argument and assign its props/fields by the fetchers props/methods
        ///     You need to specify a new fetcher for this overload the data used for the fetcher is found in the DataCollections class
        /// </summary>
        public static Func<T> CreateGenerator<T>(Action<T, Fetcher> action, Fetcher fetcher)
            where T : new() => () => {
            var t = new T();
            action(t, fetcher);
            return t;
        };

        ///<summary>
        ///     Gives a method which can be used to creating random instances of the type given
        ///     This overload can be used if you have class which do not use a constructor to set its props/fields
        ///     All you have to do is to use the type argument and assign its props/fields by the fetchers props/methods
        /// </summary>
        public static Func<T> CreateGenerator<T>(Action<T, Fetcher> action) where T : new() =>
            CreateGenerator(action, DefaultFetcher);

        ///<summary>
        ///     Gives a method which can be used to creating random instances of the type given
        ///     This overload can be used if you have a class which requires constructor arguments.
        ///     All you have to do is to new up an instance and use the fetcher to satisfy the constructor arguments.
        /// </summary>
        public static Func<T> CreateGenerator<T>(Func<Fetcher, T> func) => ()
            => func(DefaultFetcher);

        ///<summary>
        ///     Gives a method which can be used to creating random instances of the type given
        ///     This overload can be used if you have a class which requires constructor arguments.
        ///     All you have to do is to new up an instance and use the fetcher to satisfy the constructor arguments.
        ///     You need to specify a new fetcher for this overload the data used for the fetcher is found in the DataCollections class
        /// </summary>
        public static Func<T> CreateGenerator<T>(Func<Fetcher, T> func, Fetcher fetcher)
            => () => func(fetcher);


        ///<summary>
        ///     Gives a new instance of the type used
        ///     This overload can be used if you have a class which requires constructor arguments.
        ///     All you have to do is to new up an instance and use the fetcher to satisfy the constructor arguments.
        ///     You need to specify a new fetcher for this overload the data used for the fetcher is found in the DataCollections class
        /// </summary>
        public static T Generate<T>(Func<Fetcher, T> func, Fetcher fetcher) => func(fetcher);

        ///<summary>
        ///     Gives a new instance of the type used
        ///     This overload can be used if you have a class which requires constructor arguments.
        ///     All you have to do is to new up an instance and use the fetcher to satisfy the constructor arguments.
        /// </summary>
        public static T Generate<T>(Func<Fetcher, T> func) => func(DefaultFetcher);

        ///<summary>
        ///     Gives a new instance of the type used
        ///     This overload can be used if you have class which do not use a constructor to set its props/fields
        ///     All you have to do is to use the type argument and assign its props/fields by the fetchers props/methods
        /// </summary>
        public static T Generate<T>(Action<T, Fetcher> action) where T : new() =>
            Generate(action, DefaultFetcher);

        ///<summary>
        ///     Gives a new instance of the type used
        ///     This overload can be used if you have class which do not use a constructor to set its props/fields
        ///     All you have to do is to use the type argument and assign its props/fields by the fetchers props/methods
        ///     You need to specify a new fetcher for this overload the data used for the fetcher is found in the DataCollections class
        /// </summary>
        public static T Generate<T>(Action<T, Fetcher> action, Fetcher fetcher) where T : new() =>
            CreateGenerator(action, fetcher)();
    }
}