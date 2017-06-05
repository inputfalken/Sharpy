using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorAPI {
    public static class TaskExtensions {
        /// <summary>
        ///     Exposes TSource from IGenerator&lt;Task&lt;TSource&gt;&gt;.
        /// </summary>
        public static IGenerator<Task<TSource>> Do<TSource>(this IGenerator<Task<TSource>> taskGenerator,
            Action<TSource> actionTask) {
            if (actionTask == null) throw new ArgumentNullException(nameof(actionTask));
            if (taskGenerator == null) throw new ArgumentNullException(nameof(taskGenerator));
            return Generator.Function(async () => {
                var generation = await taskGenerator.Generate();
                actionTask(generation);
                return generation;
            });
        }
    }
}