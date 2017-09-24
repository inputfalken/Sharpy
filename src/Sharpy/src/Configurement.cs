using System;
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
        ///         Creates a <see cref="Configurement" /> with default implementations.
        ///     </para>
        ///     <para>
        ///         Argument <paramref name="random" /> is used by the default implementations.
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
            PhoneNumberProvider = new UniquePhoneNumberRandomizer(Random);
            UserNameProvider = new UserNameRandomizer(Data.GetUserNames, Random);
            MailProvider = new UniqueEmailBuilder(
                new[] {"gmail.com", "live.com", "outlook.com", "hotmail.com", "yahoo.com"},
                Random
            );
            ListElementPicker = new ListRandomizer(Random);
            BoolProvider = new BoolRandomizer(Random);
            ArgumentProvider = new ArgumentRandomizer(Random);
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
        public Configurement(int seed) : this(new Random(seed)) { }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Configurement" /> with default implementations.
        ///     </para>
        /// </summary>
        public Configurement() : this(new Random()) { }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IArgumentProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             By default it is <see cref="ArgumentRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IArgumentProvider ArgumentProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ISecurityNumberProvider" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             By default it is <see cref="UniqueSecurityNumberBuilder" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public ISecurityNumberProvider SecurityNumberProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IPhoneNumberProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UniquePhoneNumberRandomizer" />.
        ///     </para>
        /// </summary>
        public IPhoneNumberProvider PhoneNumberProvider { get; set; }

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
        ///         Gets and sets the implementation for <see cref="IPostalCodeProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="SwePostalCodeRandomizer" />.
        ///     </para>
        /// </summary>
        public IPostalCodeProvider PostalCodeProvider { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDateProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="DateRandomizer" />.
        ///     </para>
        /// </summary>
        public IDateProvider DateProvider { get; set; }

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
        ///         By default it is <see cref="ListRandomizer" />.
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
        ///         Gets and sets the implementation for <see cref="IIntegerProvider" />.
        ///     </para>
        /// </summary>
        /// <para>
        ///     By Default it is <see cref="IntRandomizer" />.
        /// </para>
        public IIntegerProvider IntegerProvider { get; set; }

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
    }
}