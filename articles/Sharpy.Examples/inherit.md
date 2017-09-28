You can derive from [Builder](xref:Sharpy.Builder) and extend it with your own class members.
Methods such as [AsGenerator](xref:Sharpy.Builder.AsGenerator``1(System.Func{Sharpy.Builder,``0})) and [ToGenerator](xref:Sharpy.BuilderExtensions.ToGenerator``2(``0,System.Func{``0,``1}))
will still work with the descended member, the following examples demonstrate this.

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
            IGenerator<string> gen = ExampleClass.AsGenerator((ExampleClass e) => e.MyMethod()):
        }
    }

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
            IGenerator<string> gen = new ExampleClass().ToGenerator((ExampleClass e) => e.MyMethod());
        }
    }

    class ExampleClass : Builder
    {
        public string MyMethod()
        {
            return "something to return";
        }
    }
}
```
