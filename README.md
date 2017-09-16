# Sharpy #
[![Build status](https://ci.appveyor.com/api/projects/status/7xxovtd60q5gl3ln/branch/development?svg=true)](https://ci.appveyor.com/project/inputfalken/sharpy/branch/development) [![Build Status](https://travis-ci.org/inputfalken/Sharpy.svg?branch=development)](https://travis-ci.org/inputfalken/Sharpy)

An API for generating fake data with fluent syntax.

## Example ##
```csharp
using System.Collections.Generic;
using Sharpy;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Example {
    class Program {
        static void Main(string[] args) {
            IGenerator<int> randomizer = Factory.Randomizer(min: 10, max: 100);
            IGenerator<int> doubledRandomizer = randomizer.Select(i => i * 2);

            IEnumerable<int> enumerable = randomizer
                .Take(10); // An IEnumerable containing ten randomized elements.
            IEnumerable<int> doubledEnumerable = doubledRandomizer
                .Take(10); // An IEnumerable containing ten randomized elements which has been doubled. 

        }
    }
}
```


Hope you'll find this class library useful.
## Install ##
Use the NuGet Package Manager Console and type ```Install-Package Sharpy pre```

## Todo ##

* Add more tests(WIP).
* Auto bump package version depending on branch merge.
* Generate documenation from xml comments(WIP).

[Link](https://www.nuget.org/packages/Sharpy/) to package on NuGet.
## Dependencies:

* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) For deserializing JSON.
* [MonTask](https://github.com/inputfalken/MonTask) For having a fluent API with tasks.
* [Sharpy.Core](https://github.com/inputfalken/Sharpy.Core) A fluent generation API.
