using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Sharpy.Builder.Tests {
    internal static class DependencyInjectionTestExtensions {
        internal static void VerifyServiceProvide<TExpected, TResult>(this IServiceProvider provider) {
            Assert.IsFalse(typeof(TExpected) == typeof(TResult),
                $"Type '{typeof(TExpected).FullName}' cannot be equal to '{typeof(TResult).FullName}'.");
            Assert.IsTrue(provider.GetService<TExpected>() is TResult,
                $"Expected type '{typeof(TExpected).FullName}' to be equal to '{typeof(TResult).FullName}'.");
        }
    }
}