﻿using System;
using System.Collections.Generic;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Pass an instance of this class to a generator constructor if you want to change the default behaviour of the
    ///         generator.
    ///     </para>
    /// </summary>
    public class Configurement {
        private IReadOnlyList<string> _mailDomains;

        /// <summary>
        ///     <para>
        ///         The random will be used for the Provider.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        public Configurement(Random random) {
            Random = random;
            LongProvider = new LongRandomizer(Random);
            IntegerProvider = new IntRandomizer(Random);
            DoubleProvider = new DoubleRandomizer(Random);
            NameProvider = new NameByOrigin(Random);
            DateGenerator = new DateGenerator(Random);
            SecurityNumberGen = new SecurityNumberGen(Random);
            NumberGenerator = new NumberGenerator(Random);
            MailDomains = new[] {"gmail.com", "hotmail.com", "yahoo.com"};
        }

        /// <summary>
        ///     <para>
        ///         The seed supplied will be used to instantiate System.Random, for the Provider.
        ///     </para>
        /// </summary>
        /// <param name="seed"></param>
        public Configurement(int seed) : this(new Random(seed)) { }

        /// <summary>
        ///     <para>
        ///         Creates a random by Tick.
        ///     </para>
        /// </summary>
        public Configurement() : this(new Random()) { }

        internal EmailBuilder EmailBuilder { get; set; }

        internal DateGenerator DateGenerator { get; }

        internal SecurityNumberGen SecurityNumberGen { get; }

        internal NumberGenerator NumberGenerator { get; }

        /// <summary>
        ///     <para>
        ///         Gets and Sets the implementation which Provider's FirstName, LastName methods use.
        ///     </para>
        ///     <para>
        ///         By default the names loaded from an internal file supplied by this library.
        ///     </para>
        /// </summary>
        public INameProvider NameProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and Sets the implementation which Provider's Double methods use.
        ///     </para>
        ///     <para>
        ///         By Default the doubles are randomized.
        ///     </para>
        /// </summary>
        public IDoubleProvider DoubleProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and Sets the implementation which Provider's Integer methods use.
        ///     </para>
        ///     <para>
        ///         By Default the ints are randomized.
        ///     </para>
        /// </summary>
        public IIntegerProvider IntegerProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and Sets the implementation which Provider's Long methods use.
        ///     </para>
        ///     <para>
        ///         By Default the longs are randomized.
        ///     </para>
        /// </summary>
        public ILongProvider LongProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets the Random which the Provider will use.
        ///     </para>
        /// </summary>
        public Random Random { get; }

        /// <summary>
        ///     <para>
        ///         Gets and Sets the maildomains which will be used for generating MailAddresses.
        ///     </para>
        ///     <para>
        ///         This affects Provider's MailAddress method.
        ///     </para>
        ///     <para>
        ///         Set to gmail.com, hotmail.com and yahoo.com by default.
        ///     </para>
        /// </summary>
        public IReadOnlyList<string> MailDomains {
            get { return _mailDomains; }
            set {
                EmailBuilder = new EmailBuilder(value, Random);
                _mailDomains = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and Sets if Provider's NumberByLength returns unique numbers.
        ///     </para>
        ///     <para>
        ///         Set to false by Default
        ///     </para>
        ///     <para>
        ///         NOTE:
        ///         If this is set to true the following will happen.
        ///         Provider's NumberByLength method will throw an exception if called more than Length^10
        ///     </para>
        /// </summary>
        public bool UniqueNumbers { get; set; }
    }
}