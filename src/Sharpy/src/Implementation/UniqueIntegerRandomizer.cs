using System;

namespace Sharpy.Implementation {
    internal sealed class UniqueRandomizerIntegerRandomizer : UniqueRandomizer<int> {
        internal UniqueRandomizerIntegerRandomizer(Random random)
            : base(random) { }
    }
}