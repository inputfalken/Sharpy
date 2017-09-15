using System;
using System.Collections.Generic;
using Sharpy.Core;
using Sharpy.Core.Linq;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using static Sharpy.Core.Generator;

namespace Sharpy {
    /// <summary>
    ///     Provides a set of static methods for creating <see cref="IGenerator{T}" />.
    /// </summary>
    public static class Factory {
        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         first name.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get
        ///             defaulted to <see cref="NameByOrigin" />
        ///         </para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(INameProvider provider = null) =>
            Create(provider ?? new NameByOrigin()).Select(p => p.FirstName());

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> which generates strings with numbers whose length is equal to argument:
        ///         <paramref name="length" />.
        ///         This is a static <see cref="IGenerator{T}" /> version of method: <see cref="Builder.NumberByLength" />.
        ///     </para>
        /// </summary>
        /// <param name="length">The length of the generated <see cref="string" />.</param>
        /// <param name="random">The random to generate the numbers with.</param>
        /// <param name="unique">If the numbers need to be unique.</param>
        /// <returns>
        ///     <see cref="IGenerator{T}" /> which generates strings with numbers.
        /// </returns>
        /// <exception cref="Exception">
        ///     Reached maximum amount of combinations for the argument <paramref name="length" />.
        ///     This should only occur if argument: <paramref name="unique" /> is set to true.
        /// </exception>
        public static IGenerator<string> NumberByLength(int length, Random random, bool unique) {
            var exceptionMessage = $"Reached maximum amount of combinations for the argument '{nameof(length)}'.";
            return
                Create((NumberGenerator: new NumberGenerator(random), Pow: (int) Math.Pow(10, length) - 1))
                    .Select(arg => arg.NumberGenerator.RandomNumber(0, arg.Pow, unique))
                    .Select(number => number == -1
                        ? throw new Exception(exceptionMessage)
                        : number.ToString())
                    .Select(number => number.Length != length ? number.Prefix(length - number.Length) : number);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         first name based on argument <see cref="Gender" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get
        ///             defaulted to <see cref="NameByOrigin" />
        ///         </para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(Gender gender,
            INameProvider provider = null) => Create(provider ?? new NameByOrigin()).Select(p => p.FirstName(gender));

        /// <summary>
        ///     Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.
        ///     Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a last
        ///     name.
        ///     <para> </para>
        ///     <remarks>
        ///         If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get defaulted to
        ///         <see cref="NameByOrigin" />
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> LastName(
            INameProvider lastNameProvider = null) =>
            Create(lastNameProvider ?? new NameByOrigin()).Select(p => p.LastName());

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        ///     <param name="seed">
        ///         A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///         number is specified, the absolute value of the number is used
        ///     </param>
        /// </summary>
        public static IGenerator<string> Username(Random rnd) => Create(new Builder(new Configurement(rnd)))
            .Select(p => p.UserName());

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        /// </summary>
        public static IGenerator<string> Username() => Create(new Builder())
            .Select(p => p.UserName());

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> by using <see cref="Sharpy.Builder" /> in a <paramref name="selector" />
        ///     function.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The result type from the <paramref name="selector" /> function.
        /// </typeparam>
        /// <param name="selector">
        ///     A transform function to apply to each generated element.
        /// </param>
        /// <param name="configurement">
        ///     A configuration class used by <see cref="Sharpy.Builder" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose type has is the result from using a <paramref name="selector" /> function
        ///     combined with <see cref="Sharpy.Builder" />.
        /// </returns>
        public static IGenerator<TResult> Builder<TResult>(Func<Builder, TResult> selector,
            Configurement configurement) => configurement != null
            ? selector != null
                ? Create(new Builder(configurement)).Select(selector)
                : throw new ArgumentNullException(nameof(selector))
            : throw new ArgumentNullException(nameof(configurement));

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> by using <see cref="Sharpy.Builder" /> in a <paramref name="selector" />
        ///     function.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The result type from the <paramref name="selector" /> function.
        /// </typeparam>
        /// <param name="selector">
        ///     A transform function to apply to each generated element.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose type has is the result from using a <paramref name="selector" /> function
        ///     combined with <see cref="Sharpy.Builder" />.
        /// </returns>
        public static IGenerator<TResult> Builder<TResult>(Func<Builder, TResult> selector) =>
            Builder(selector, new Configurement());

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> whose generations will create strings representing email addresses.
        /// </summary>
        /// <param name="domains">The email domains to generate.</param>
        /// <param name="random">The randomizer.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who generates strings representing email addresses.
        /// </returns>
        public static IGenerator<string> Email(IReadOnlyList<string> domains = null, Random random = null) {
            var rnd = random ?? new Random();
            return Create(new EmailBuilder(domains ?? new[] {"gmail.com", "yahoo", "hotmail.com", "outlook.com"}, rnd))
                .Zip(Username(rnd), (builder, s) => builder.Mail(s, null));
        }
    }
}