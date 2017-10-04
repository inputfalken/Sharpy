You can derive from [Builder](xref:Sharpy.Builder) and extend it with your own class members.
Methods such as [AsGenerator](xref:Sharpy.Builder.AsGenerator``1(System.Func{Sharpy.Builder,``0})), [ToEnumerable](xref: Sharpy.BuilderExtensions.ToEnumerable``2(``0,System.Func{``0,System.Int32,``1},System.Int32)) and [ToGenerator](xref:Sharpy.BuilderExtensions.ToGenerator``2(``0,System.Func{``0,``1}))
will have access to the descended class members as you will see on the following examples.

## Calling AsGenerator ##
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
            ExampleClass example = new ExampleClass();
            // Creates a IGenerator<string> which will invoke MyMethod.
            IGenerator<string> gen = example.AsGenerator((ExampleClass e) => e.MyMethod()):
        }
    }

    // Example class deriving from Builder.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```

## Calling ToGenerator ##
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
            ExampleClass example = new ExampleClass();
            // Creates a IGenerator<string> which will invoke MyMethod.
            IGenerator<string> gen = example.ToGenerator((ExampleClass e) => e.MyMethod());
        }
    }

    // Example class deriving from Builder.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}

```
## Calling ToEnumerable ##
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
            ExampleClass example = new ExampleClass();
            // Creates an IEnumerable<string> with 20 elements.
            IEnumerable<string> gen = example.ToEnumerable((ExampleClass e) => e.MyMethod(), 20);

        }
    }

    // Example class deriving from Builder.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```
