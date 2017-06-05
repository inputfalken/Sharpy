using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorAPI {
    public static class TaskExtensions {
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> firstTaskGenerator,
            IGenerator<TSecond> second, Func<TFirst, TSecond, TResult> composer) {
            return firstTaskGenerator.Zip(second, async (l, r) => composer(await l, r));
        }

        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(this IGenerator<TFirst> first,
            IGenerator<Task<TSecond>> secondTaskGenerator, Func<TFirst, TSecond, TResult> composer) {
            return first.Zip(secondTaskGenerator, async (l, r) => composer(l, await r));
        }

        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> firstTaskGenerator,
            IGenerator<Task<TSecond>> secondTaskGenerator, Func<TFirst, TSecond, TResult> composer) {
            return firstTaskGenerator.Zip(secondTaskGenerator, async (l, r) => composer(await l, await r));
        }
    }
}