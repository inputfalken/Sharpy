# Information #

There's currently two libraries which this website contains information about:
* **Sharpy.Core** Offers an interface which offers fluent syntax for it's implementations.
 With LINQ naming convention.

* **Sharpy:** Offers various methods for generating data. Such as names and numbers.
 Sharpy also uses **Sharpy.Core** so you can have the best of both worlds.


# Examples #

## Generating First Names ##

```csharp
using System;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Core;
using System.Generic.Collections;
namspace Example
{
    class Program
    {
        public static void Main (string[] args)
        {
            IEnumerable<string> names = Generator.Create(new Builder()) // Creates a IGenerator<Builder>.
            .Select((Builder builder) => builder.FirstName(Gender.Male)) // Maps the IGenerator<Builder> to IGenerator<string>.
            .Take(100); // Returns a lazy evaluated IEnumerable<string> with 100 names.
        }
    }
}
```
## Generating Numbers ##

```csharp
using System;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Core;
using System.Generic.Collections;
namspace Example
{
    class Program
    {
        public static void Main (string[] args)
        {
            IEnumerable<string> names = Generator.Create(new Builder()) // Creates a IGenerator<Builder>.
            .Select((Builder builder) => builder.Integer(10, 100)) // Maps the IGenerator<Builder> to IGenerator<int>.
            .Take(100); // Returns a lazy evaluated IEnumerable<int> with 100 integers.
        }
    }
}
```
