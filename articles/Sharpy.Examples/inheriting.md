These examples require you to have Sharpy
If you don't have Sharpy installed, please visit [Getting Started](./getting.started.md)

You can derive from [Builder](xref:Sharpy.Builder) and extend it with your own class members.
Methods such as [Generator](xref:Sharpy.BuilderFactory.Generator``1(System.Func{Sharpy.Builder,``0})), [ToEnumerable](xref: Sharpy.BuilderExtensions.ToEnumerable``2(``0,System.Func{``0,System.Int32,``1},System.Int32)) and [Generator](xref:Sharpy.BuilderExtensions.ToGenerator``2(``0,System.Func{``0,``1}))
will use descended class members and let you use the class members of both builder and the descended type, as you will see on the following members.

## Calling Generator ##

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
            // Creates a IGenerator<string> which will invoke MyMethod.
            IGenerator<string> gen = ExampleClass.Generator(TBuilderFactory: new ExampleClass() , selector: (ExampleClass: e) => e.MyMethod()):
        }
    }

    // Example class deriving from BuilderFactory.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```

## Calling Generator ##

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
            IGenerator<string> gen = example.Generator(selector: (ExampleClass e) => e.MyMethod());
        }
    }

    // Example class deriving from BuilderFactory.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}

```
## Calling Enumerable ##

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
            IEnumerable<string> gen = example.Enumerable(selector: (ExampleClass e) => e.MyMethod(), count: 20);

        }
    }

    // Example class deriving from BuilderFactory.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```
