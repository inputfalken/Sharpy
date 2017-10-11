These examples require you to have Sharpy
If you don't have Sharpy installed, please visit [Getting Started](./getting.started.md)

The easiest way to get started with is to use the [BuilderFactory](xref:Sharpy.BuilderFactory).
This class contains methods to create objects using [Builder](xref:Sharpy.Builder.Builder)
from the [Sharpy.Builder](xref:Sharpy.Builder) project.

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
            IEnumerable<string> names = BuilderFactory
            .Generator(selector: (BuilderFactory builder) => builder.FirstName(gender: Gender.Male))
            .take(count: 100);
        }
    }
}
```
this example uses the following methods:
* [Generator](xref:Sharpy.BuilderFactory.Generator``2(``0,System.Func{``0,``1}))
* [FirstName](xref:Sharpy.BuilderFactory.FirstName)
* [take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))

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
            IEnumerable<int> names = BuilderFactory
            .Generator(selector: (BuilderFactory builder) => builder.Integer(min: 10, max: 100))
            .take(count: 100);
        }
    }
}
```
this example use the following methods:
* [Generator](xref:Sharpy.BuilderFactory.Generator``2(``0,System.Func{``0,``1}))
* [Integer](xref:Sharpy.BuilderFactory.Integer(System.Int32,System.Int32))
* [take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))


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
            IGenerator<Person> generator = BuilderFactory.Generator(selector: (Builder builder) =>
                new Person(
                    firstname: builder.FirstName(),
                    lastname: builder.LastName(),
                    age: builder.Integer(min: 20, max: 60)
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
this example use the following methods:
* [Generator](xref:Sharpy.BuilderFactory.Generator``2(``0,System.Func{``0,``1}))
* [FirstName](xref:Sharpy.BuilderFactory.FirstName)
* [Integer](xref:Sharpy.BuilderFactory.Integer(System.Int32,System.Int32))
* [LastName](xref:Sharpy.BuilderFactory.LastName)
* [take](xref:Sharpy.Core.Linq.Extensions.Take``1(Sharpy.Core.IGenerator{``0},System.Int32))
