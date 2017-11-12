These examples require you to have Sharpy
If you don't have Sharpy installed, please visit [Getting Started](./getting.started.md)

You can derive from [Builder](xref:Sharpy.Builder) and extend it with your own class members.
Methods such as [Generator](xref:Sharpy.BuilderFactory.Generator``2(``0,System.Func{``0,``1})), [Enumerable](xref:Sharpy.BuilderFactory.Enumerable``2(``0,System.Func{``0,``1},System.Int32)) and [Generator](xref:Sharpy.BuilderFactory.Generator``2(``0,System.Func{``0,``1}))
will use descended class members and let you use the class members of both builder and the descended type, as you will see on the following members.

## Calling Generator

### As a static method

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
            IGenerator<string> gen = BuilderFactory.Generator(
                TBuilder: new ExampleClass(),
                selector: (ExampleClass: e) => e.MyMethod()
            ):
        }
    }

    // Example class deriving from builder.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```

### As an extension method

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
            IGenerator<string> gen = example.Generator(
                selector: (ExampleClass e) => e.MyMethod()
            );
        }
    }

    // Example class deriving from builder.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}

```

## Calling Enumerable

## As a static method

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
            // Creates an IEnumerable<string> with 20 elements.
            IEnumerable<string> gen = BuilderFactory.Enumerable(
                TBuilder: new ExampleClass(),
                selector: (ExampleClass e) => e.MyMethod(),
                count: 20
            );

        }
    }

    // Example class deriving from builder.
    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```

### As an extension method

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
            IEnumerable<string> gen = example.Enumerable(
                selector: (ExampleClass e) => e.MyMethod(),
                count: 20
            );

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
