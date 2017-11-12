These examples require you to have Sharpy.Builder or Sharpy
If you don't have Sharpy installed, please visit [Getting Started](./getting.started.md)

You can configure the [Builder](xref:Sharpy.Builder) by passing a [Configurement](xref:Sharpy.Builder.Configurement) instance to the constructor.
The [Configurement](xref:Sharpy.Builder.Configurement) class contains interface properties which you can overwrite to change the behaviour of the [Builder](xref:Sharpy.Builder).
The following example demonstrates how you could configure the [Builder](xref:Sharpy.Builder).

## Configuring NameProvider

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
            Configurement config = new Configurement
            {
                // the builder's FirstName & LastName -
                // methods will only return common names from the United States.
                NameProvider = new NameByOrigin(origins: Origin.UnitedStates)
            };

            var builder = new Builder(config);
        }
    }
}

```

## Configuring IntegerProvider

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
            const int seed = 100;
            Configurement config = new Configurement
            {
                // the builder's Integer methods will now -
                // randomize with the seed provided.
                IntegerProvider = new IntRandomizer(random: new Random(seed))
            };

            var builder = new Builder(config);
        }
    }
}

```
