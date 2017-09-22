﻿using System;
using System.Collections.Generic;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Pass an instance of this class to a <see cref="Builder" /> constructor if you want to change the default
    ///         behavior of the
    ///         <see cref="Builder" />.
    ///     </para>
    /// </summary>
    public class Configurement {
        /// <summary>
        ///     <para>
        ///         The argument <paramref name="random" /> will be used for the <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        public Configurement(Random random) {
            Random = random;
            LongProvider = new LongRandomizer(Random);
            IntegerProvider = new IntRandomizer(Random);
            DoubleProvider = new DoubleRandomizer(Random);
            NameProvider = new NameByOrigin(Random);
            DateProvider = new DateRandomizer(Random);
            SecurityNumberProvider = new UniqueFormattedSecurityBuilder(Random);
            PostalCodeProvider = new SwePostalCodeRandomizer(Random);
            UniqueRandomizerIntegerRandomizer = new UniqueRandomizerIntegerRandomizer(Random);
            UserNameProvider = new UserNameRandomizer(Data.GetUserNames, Random);
            MailProvider = new UniqueEmailBuilder(
                new[] {"gmail.com", "live.com", "outlook.com", "hotmail.com", "yahoo.com"},
                Random
            );
            ListElementPicker = new ListRandomizer(Random);
            BoolProvider = new BoolRandomizer(Random);
        }

        /// <summary>
        ///     <para>
        ///         The seed supplied will be used to instantiate a <see cref="Random" />, for the <see cref="Builder" />.
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

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ISecurityNumberProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="UniqueSecurityNumberBuilder" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public ISecurityNumberProvider SecurityNumberProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IUserNameProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="UserNameRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IUserNameProvider UserNameProvider { get; set; }

        internal UniqueRandomizerIntegerRandomizer UniqueRandomizerIntegerRandomizer { get; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IPostalCodeProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="SwePostalCodeRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IPostalCodeProvider PostalCodeProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDateProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="DateRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IDateProvider DateProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IEmailProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="UniqueEmailBuilder" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IEmailProvider MailProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IReadListElementProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="ListRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IReadListElementProvider ListElementPicker { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IBoolProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="BoolRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IBoolProvider BoolProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="INameProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="NameByOrigin" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public INameProvider NameProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDoubleProvider" />
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="DoubleRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IDoubleProvider DoubleProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IIntegerProvider" />.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The default implementation is <see cref="IntRandomizer" />.
        ///     </para>
        /// </remarks>
        public IIntegerProvider IntegerProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ILongProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="LongRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public ILongProvider LongProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets the Random which the <see cref="Builder" /> will use.
        ///     </para>
        /// </summary>
        public Random Random { get; }

        /// <summary>
        ///     <para>
        ///         Gets and sets if <see cref="Builder.NumberByLength" /> returns unique numbers.
        ///         Set to false by Default
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             If this is set to true the following will happen.
        ///             <see cref="Builder.NumberByLength" /> method will throw an exception if called more than Length^10
        ///         </para>
        ///     </remarks>
        /// </summary>
        public bool UniqueNumbers { get; set; }
    }
}