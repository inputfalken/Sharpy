using System;
using GeneratorAPI;
using GeneratorAPI.Linq;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    ///     Provides a set of static methods for creating <see cref="IGenerator{T}"/>.
    /// </summary>
    public static class GeneratorFactory {
        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="provider"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IGenerator<TResult> ToGenerator<TProvider, TResult>(this TProvider provider,
            Func<TProvider, TResult> selector) where TProvider : Provider {
            return Generator.Create(provider).Select(selector);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invokation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         first name.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get
        ///             defaulted to <see cref="NameByOrigin" />
        ///         </para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(provider.FirstName);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invokation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
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
            INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(() => provider.FirstName(gender));
        }

        /// <summary>
        ///     Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.
        ///     Each invokation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a last
        ///     name.
        ///     <para> </para>
        ///     <remarks>
        ///         If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get defaulted to
        ///         <see cref="NameByOrigin" />
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> LastName(
            INameProvider lastNameProvider = null) {
            lastNameProvider = lastNameProvider ?? new NameByOrigin();
            return Generator.Function(lastNameProvider.LastName);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invokation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        ///     <param name="seed">
        ///         A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///         number is specified, the absolute value of the number is used
        ///     </param>
        /// </summary>
        public static IGenerator<string> Username(int seed) {
            return Generator
                .Create(new Provider(seed))
                .Select(p => p.UserName());
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invokation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        /// </summary>
        public static IGenerator<string> Username() {
            return Generator
                .Create(new Provider())
                .Select(p => p.UserName());
        }
    }
}