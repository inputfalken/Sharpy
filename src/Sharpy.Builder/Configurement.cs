﻿using System;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder
{
    /// <summary>
    ///     <para>
    ///         Pass an instance of this class to a <see cref="Builder" /> constructor if you want to change the default
    ///         behavior of the
    ///         <see cref="Builder" />.
    ///     </para>
    /// </summary>
    public class Configurement
    {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Configurement" /> with default implementations.
        ///     </para>
        ///     <para>
        ///         Argument <paramref name="random" /> is used by the default implementations.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        public Configurement(Random random)
        {
            Random = random;
            LongProvider = new LongRandomizer(Random);
            IntProvider = new IntRandomizer(Random);
            DoubleProvider = new DoubleRandomizer(Random);
            NameProvider = new NameByOrigin(Random);
            DateTimeProvider = new DateTimeRandomizer(Random);
            UserNameProvider = new UserNameRandomizer(Random);
            MailProvider = new UniqueEmailBuilder(
                new[] {"gmail.com", "live.com", "outlook.com", "hotmail.com", "yahoo.com"}
            );
            ListElementPicker = new ElementRandomizer(Random);
            BoolProvider = new BoolRandomizer(Random);
            GuidProvider = new GuidProvider();
            TimeSpanProvider = new TimeSpanRandomizer(Random);
            DecimalProvider = new DecimalRandomizer(Random);
            DateTimeOffSetProvider = new DateTimeOffsetRandomizer(Random);
            FloatProvider = new FloatRandomizer(Random);
            CharProvider = new CharRandomizer(Random);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Configurement" /> with default implementations.
        ///     </para>
        ///     <para>
        ///         The seed supplied will be used to instantiate a <see cref="Random" /> for the default implementations.
        ///     </para>
        /// </summary>
        /// <param name="seed"></param>
        public Configurement(int seed) : this(new Random(seed))
        {
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Configurement" /> with default implementations.
        ///     </para>
        /// </summary>
        public Configurement() : this(new Random())
        {
        }


        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IUserNameProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UserNameRandomizer" />.
        ///     </para>
        /// </summary>
        public IUserNameProvider UserNameProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDateTimeProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="DateTimeRandomizer" />.
        ///     </para>
        /// </summary>
        public IDateTimeProvider DateTimeProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IEmailProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UniqueEmailBuilder" />.
        ///     </para>
        /// </summary>
        public IEmailProvider MailProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IElementProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="ElementRandomizer" />.
        ///     </para>
        /// </summary>
        public IElementProvider ListElementPicker { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IBoolProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="BoolRandomizer" />.
        ///     </para>
        /// </summary>
        public IBoolProvider BoolProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="INameProvider" />.
        ///     </para>
        ///     <para>
        ///         By Default it is <see cref="NameByOrigin" />.
        ///     </para>
        /// </summary>
        public INameProvider NameProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDoubleProvider" />
        ///     </para>
        ///     <para>
        ///         By Default it is <see cref="DoubleRandomizer" />.
        ///     </para>
        /// </summary>
        public IDoubleProvider DoubleProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IIntProvider" />.
        ///     </para>
        ///     <para>
        ///         By Default it is <see cref="IntRandomizer" />.
        ///     </para>
        /// </summary>
        public IIntProvider IntProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ILongProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="LongRandomizer" />.
        ///     </para>
        /// </summary>
        public ILongProvider LongProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets the <see cref="System.Random" /> for the default implementations of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        public Random Random { get; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IGuidProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="Sharpy.Builder.Implementation.GuidProvider" />.
        ///     </para>
        /// </summary>
        public IGuidProvider GuidProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ITimeSpanProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="Sharpy.Builder.Implementation.TimeSpanRandomizer" />.
        ///     </para>
        /// </summary>
        public ITimeSpanProvider TimeSpanProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDecimalProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="Sharpy.Builder.Implementation.DecimalRandomizer" />.
        ///     </para>
        /// </summary>
        public IDecimalProvider DecimalProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDateTimeOffsetProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="Sharpy.Builder.Implementation.DateTimeOffsetRandomizer" />.
        ///     </para>
        /// </summary>
        public IDateTimeOffsetProvider DateTimeOffSetProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IFloatProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="Sharpy.Builder.Implementation.FloatRandomizer" />.
        ///     </para>
        /// </summary>
        public IFloatProvider FloatProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ICharProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="Sharpy.Builder.Implementation.CharRandomizer" />.
        ///     </para>
        /// </summary>
        public ICharProvider CharProvider { get; set; }
    }
}