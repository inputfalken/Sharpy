You can configure the [Builder](xref:Sharpy.Builder) by passing a [Configurement](xref:Sharpy.Configurement) instance to the constructor of the [Builder](xref:Sharpy.Builder).
The [Configurement](xref:Sharpy.Configurement) class contains interface properties which you can overwrite to change the behaviour of the [Builder](xref:Sharpy.Builder).
The following example demonstrates how you could configure the [Builder](xref:Sharpy.Builder).

## Configuring NameProvider ##

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
                // The builder's FirstName & LastName methods will only return common names from the United States.
                NameProvider = new NameByOrigin(Origin.UnitedStates)
            };

            IEnumerable<string> names = Builder
            .AsGenerator(new Builder(config), builder => builder.FirstName(Gender.Female))
            .Take(30);
        }
    }
}

```
This example use the following methods:
* [AsGenerator](xref:Sharpy.Builder.AsGenerator``1(System.Func{Sharpy.Builder,``0}))
* [FirstName](xref:Sharpy.Builder.FirstName(Gender))
* [Take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))
