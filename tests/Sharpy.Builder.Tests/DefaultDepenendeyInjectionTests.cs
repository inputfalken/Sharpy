using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Tests {
    [TestFixture]
    public class DefaultDepenendeyInjectionTests {
        private readonly IServiceProvider _provider = new Configurement().Providers.BuildServiceProvider();

        /// <summary>
        /// Verifies that default serviceproviders works as expected.
        /// </summary>
        /// <typeparam name="TExpected"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        private void VerifyDefaultServiceProvider<TExpected, TResult>() {
            Assert.IsFalse(typeof(TExpected) == typeof(TResult),
                $"Type '{typeof(TExpected).FullName}' cannot be equal to '{typeof(TResult).FullName}'.");
            Assert.IsTrue(_provider.GetService<TExpected>() is TResult,
                $"Expected type '{typeof(TExpected).FullName}' to be equal to '{typeof(TResult).FullName}'.");
        }

        [Test]
        public void Default_IntegerProvider() => VerifyDefaultServiceProvider<IIntegerProvider, IntegerRandomizer>();

        [Test]
        public void Default_LongProvider() => VerifyDefaultServiceProvider<ILongProvider, LongRandomizer>();

        [Test]
        public void Default_DoubleProvider() => VerifyDefaultServiceProvider<IDoubleProvider, DoubleRandomizer>();

        [Test]
        public void Default_BoolProvider() => VerifyDefaultServiceProvider<IBoolProvider, BoolRandomizer>();

        [Test]
        public void Default_IUserNameProvider() =>
            VerifyDefaultServiceProvider<IUserNameProvider, UserNameRandomizer>();

        [Test]
        public void Default_NameProvider() => VerifyDefaultServiceProvider<INameProvider, NameByOrigin>();

        [Test]
        public void Default_EmailProvider() => VerifyDefaultServiceProvider<IEmailProvider, UniqueEmailBuilder>();

        [Test]
        public void Default_ArgumentProvider() => VerifyDefaultServiceProvider<IArgumentProvider, ArgumentRandomizer>();

        [Test]
        public void Default_ElementProvider() => VerifyDefaultServiceProvider<IElementProvider, ListRandomizer>();

        [Test]
        public void Default_PostalCodeProvider() =>
            VerifyDefaultServiceProvider<IPostalCodeProvider, SwePostalCodeRandomizer>();

        [Test]
        public void Default_DateProvider() => VerifyDefaultServiceProvider<IDateProvider, DateRandomizer>();

        [Test]
        public void Default_PhoneNumberProvider() =>
            VerifyDefaultServiceProvider<IPhoneNumberProvider, UniquePhoneNumberRandomizer>();

        [Test]
        public void Default_SecurityNumberProvider() =>
            VerifyDefaultServiceProvider<ISecurityNumberProvider, UniqueSecurityNumberBuilder>();
        [Test]
        public void Default_MovieDbProvider() =>
            VerifyDefaultServiceProvider<IMovieDbProvider, MovieDbRandomizer>();

        [Test]
        public void Bulder_Using_ServiceProvider_Constructor_Does_Not_Throw() => Assert.DoesNotThrow(() => new Builder(_provider));
    }
}