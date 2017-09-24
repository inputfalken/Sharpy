These examples require you to have Sharpy
If you don't have Sharpy installed, please visit [Getting Started](./getting.started.md)

The main object you should use is the [Builder](xref:Sharpy.Builder) object.
This object contains various methods that is useful when you want to generate data.
The following examples will demonstrate how you could use the builder object.


## Generating First Names ##

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
            IEnumerable<string> names = Builder
            .AsGenerator((Builder builder) => builder.FirstName(Gender.Male))
            .Take(100);
        }
    }
}
```
This example uses the following methods:
* [AsGenerator](xref:Sharpy.Builder.AsGenerator``2(``0,System.Func{``0,``1}))
* [FirstName](xref:Sharpy.Builder.FirstName)
* [Take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))

## Generating Numbers ##

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
            IEnumerable<int> names = Builder
            .AsGenerator((Builder builder) => builder.Integer(10, 100))
            .Take(100);
        }
    }
}
```
This example use the following methods:
* [AsGenerator](xref:Sharpy.Builder.AsGenerator``2(``0,System.Func{``0,``1}))
* [Integer](xref:Sharpy.Builder.Integer(System.Int32,System.Int32))
* [Take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))


## Generating Your Own Type ##
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
            IGenerator<Person> generator = Builder.AsGenerator((Builder builder) =>
                new Person(
                    firstname: builder.FirstName(),
                    lastname: builder.LastName(),
                    age: builder.Integer(20, 60)
                )
            );

            IEnumerable<Person> people = generator.Take(100);
        }
    }

    class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public Person (string firstname, string lastname, int age)
        {
            FirstName = firstname;
            LastName = lastname;
            Age = age;
        }

    }
}
```
This example use the following methods:
* [AsGenerator](xref:Sharpy.Builder.AsGenerator``2(``0,System.Func{``0,``1}))
* [FirstName](xref:Sharpy.Builder.FirstName)
* [Integer](xref:Sharpy.Builder.Integer(System.Int32,System.Int32))
* [LastName](xref:Sharpy.Builder.LastName)
* [Take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))
