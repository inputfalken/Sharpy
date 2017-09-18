using System;
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
        private IReadOnlyList<string> _mailDomains;

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
            SecurityNumberGen = new SecurityNumberGen(Random);
            NumberGenerator = new NumberGenerator(Random);
            MailDomains = new[] {"gmail.com", "hotmail.com", "yahoo.com"};
            ListElementPicker = new ListRandomizer(random);
            BoolProvider = new BoolRandomizer(random);
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

        internal SecurityNumberGen SecurityNumberGen { get; }

        internal NumberGenerator NumberGenerator { get; }

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
        ///         Gets and sets the implementation for <see cref="IListElementPicker" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             The default implementation is <see cref="ListRandomizer" />.
        ///         </para>
        ///     </remarks>
        /// </summary>
        public IListElementPicker ListElementPicker { get; set; }

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
        ///         Gets and sets the mail domains which will be used when invoking <see cref="Builder.Mail" />.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The default values is gmail.com, hotmail.com and yahoo.com.
        ///     </para>
        /// </remarks>
        public IReadOnlyList<string> MailDomains {
            get => _mailDomains;
            set {
                MailProvider = new UniqueEmailBuilder(value, Random);
                _mailDomains = value;
            }
        }

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