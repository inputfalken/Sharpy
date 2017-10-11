These examples require you to have Sharpy.Core or Sharpy
If you don't have Sharpy installed, please visit [Getting Started](./getting.started.md)

this page contains examples how you could create your own generation algorithms.

## Creating a generator based on System.Random
this example use a factory method which
lets you create a IGenerator based on a function.
the function you give will be invoked for every invocation of [Generate](xref:Sharpy.Core.IGenerator`1.Generate)
```csharp
using Sharpy.Core;
using Sharpy.Enums;
using Sharpy;
using System.Generic.Collections;
using System;
namespace Example
{
    class Program
    {
        public static void Main (string[] args)
        {
            IEnumerable<int> genereator = RandomGenerator().Take(100);
        }

        public static IGenerator<int> RandomGenerator()
        {
            Random rnd = new Random();
            return Generator.Function(() => rnd.Next());
        }
    }
}
```
this example use the following methods:
* [Take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))
* [Function](xref:Sharpy.Core.Generator.Function``1(System.Func{``0}))

You can achieve the same result by implementing the IGenerator interface as well.

```csharp
using Sharpy.Core;
using Sharpy.Enums;
using Sharpy;
using System.Generic.Collections;
using System;
namespace Example
{
    class Program
    {
        public static void Main (string[] args)
        {
            IEnumerable<int> genereator = new RandomGenerator(new Random()).Take(100);
        }
    }

    class RandomGenerator : IGenerator<int>
    {
        private readonly Random _rnd;
        RandomGenerator(Random rnd) => _rnd = rnd;
        public int Generate() => rnd.Next();
    }
}
```
this example use the following methods:
* [Take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))
