using System;
using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///    Inherits from Generator class with the type signature &lt;T,StringType&gt;.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SharpyGenerator<T> : Generator<T, Randomizer> {
        /// <summary>
        ///    Uses my own implementation of IRandomizer&lt;StringType&gt; Uses an optional config argument as secondary argument.
        /// </summary>
        public SharpyGenerator(Func<IRandomizer<StringType>, T> func, Config config = null)
            : base(func, new Randomizer(config ?? new Config())) {}

        /// <summary>
        ///    Uses my own implementation of IRandomizer&lt;StringType&gt; Uses an optional config argument as secondary argument.
        ///    Includes an integer which is equal to ammount of iterations done. starts on number zero.
        /// </summary>
        public SharpyGenerator(Func<IRandomizer<StringType>, int, T> func, Config config = null)
            : base(func, new Randomizer(config ?? new Config())) {}
    }
}