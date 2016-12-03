using System;
using System.Collections.Generic;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Use this class if you want to configure your Generator. then call CreateGenerator to get the generator.
    ///     </para>
    /// </summary>
    public class Configurement {
        /// <summary>
        ///     <para>The random will be used for the Generator.</para>
        /// </summary>
        /// <param name="random"></param>
        public Configurement(Random random) {
            Random = random;
        }

        /// <summary>
        ///     <para>The seed supplied will be used to instantiate System.Random. For the Generator.</para>
        /// </summary>
        /// <param name="seed"></param>
        public Configurement(int seed) : this(new Random(seed)) {}

        /// <summary>
        ///     <para>Creates a random by Tick </para>
        /// </summary>
        public Configurement() : this(new Random()) {}

        /// <summary>
        ///     <para>Gets and Sets the implementation which Generator's FirstName, LastName methods use.</para>
        ///     <para>By default the names loaded from an internal file supplied by this library.</para>
        /// </summary>
        public INameProvider NameProvider { get; set; }

        /// <summary>
        ///     <para>Gets and Sets the implementation which Generator's Double methods use.</para>
        ///     <para>By Default the doubles are randomized</para>
        /// </summary>
        public IDoubleProvider DoubleProvider { get; set; }

        /// <summary>
        ///     <para>Gets and Sets the implementation which Generator's Integer methods use.</para>
        ///     <para>By Default the ints are randomized</para>
        /// </summary>
        public IIntegerProvider IntegerProvider { get; set; }

        /// <summary>
        ///     <para>Gets and Sets the implementation which Generator's Long methods use.</para>
        ///     <para>By Default the longs are randomized</para>
        /// </summary>
        public ILongProvider LongProvider { get; set; }

        /// <summary>
        ///     <para>Gets the Random which the Generator will use.</para>
        /// </summary>
        public Random Random { get; }

        /// <summary>
        ///     <para>Gets and Sets the mailproviders which will be used for generating MailAddresses.</para>
        ///     <para>This affects Generator's MailAddress method.</para>
        ///     <para>Set to gmail.com, hotmail.com and yahoo.com by default.</para>
        /// </summary>
        public IReadOnlyList<string> MailProviders { get; set; } = new[] {"gmail.com", "hotmail.com", "yahoo.com"};

        /// <summary>
        ///     <para>Gets and Sets if Generator's NumberByLength returns unique numbers.</para>
        ///     <para>Set to false by Default</para>
        ///     <para>
        ///         NOTE:
        ///         If this is set to true the following will happen.
        ///         Generator's NumberByLength method will throw an exception if called more than Length^10
        ///     </para>
        /// </summary>
        public bool UniqueNumbers { get; set; }

        /// <summary>
        ///     Creates a Generator with your configurement.
        /// </summary>
        /// <returns></returns>
        public Generator Create() {
            if (LongProvider == null) LongProvider = new LongRandomizer(Random);
            if (IntegerProvider == null) IntegerProvider = new IntRandomizer(Random);
            if (DoubleProvider == null) DoubleProvider = new DoubleRandomizer(Random);
            if (NameProvider == null) NameProvider = new NameByOrigin(Random);
            return new Generator(Random, DoubleProvider, IntegerProvider, LongProvider, NameProvider, MailProviders, UniqueNumbers);
        }
    }
}