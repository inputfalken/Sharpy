using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorAPI {
    internal static class TaskExtensions {
        public static IGenerator<Task<TSource>> Where<TSource>(this IGenerator<Task<TSource>> taskGenerator,
            Func<TSource, bool> predicate, int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (taskGenerator == null) throw new ArgumentNullException(nameof(taskGenerator));
            // Duplicated from Generator.Where but with async.
            return Generator.Function(async () => {
                for (var i = 0; i < threshold; i++) {
                    var generation = await taskGenerator.Generate();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
        }
    }
}