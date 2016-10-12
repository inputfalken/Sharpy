# Sharpy

The idea of this project is to let users have a source to fetch random data from.

####Features

* LocalDate
* Bool
* Name
* Int
* Mail
* Phone number

###Examples
#### Generating
```C#
using Sharpy.Enums;
using Sharpy.Types;

namespace Logger {
  internal static class Program {
    public static void Main() {
      var generator = new Generator<Person>(randomizer => new Person {
          FirstName = randomizer.Name(NameType.MixedFirstName),
          LastName = randomizer.Name(NameType.LastName)
          });

      // Creates an IEnumerable<Person> with twenty persons. All with randomized names.
      var persons = generator.GenerateEnumerable(20);
      // Creates one person with randomized names.
      var person = generator.Generate();
    }
  }

  internal class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
```
#### Configure & Generating
```C#
using Sharpy.Enums;
using Sharpy.Types;

namespace Logger {
  internal static class Program {
    public static void Main() {
      var generator = new Generator<Person>(randomizer => new Person {
          FirstName = randomizer.Name(NameType.MixedFirstName),
          LastName = randomizer.Name(NameType.LastName)
          });

      // Limits the name usage to common names in United States.
      generator.ConfigName(Country.UnitedStates);
      // Creates an IEnumerable<Person> with twenty persons. All with randomized names.
      var persons = generator.GenerateEnumerable(20);
      // Creates one person with randomized names.
      var person = generator.Generate();
    }
  }

  internal class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
```
#### Supplying your own collection
```C#
using Sharpy;
using Sharpy.Enums;

namespace Logger {
  internal static class Program {
    public static void Main() {
      string[] workplaces = {
        "workplace1", "workplace2", "workplace3",
        "workplace4", "workplace5", "workplace6"
      };
      var generator = new Generator<Person>(randomizer => new Person {
          FirstName = randomizer.Name(NameType.MixedFirstName),
          LastName = randomizer.Name(NameType.LastName),
          WorkPlace = randomizer.CustomCollection(workplaces)
          });

      // Limits the name usage to common names in United States.
      generator.ConfigName(Country.UnitedStates);
      // Creates an IEnumerable<Person> with twenty persons. All with randomized names.
      var persons = generator.GenerateEnumerable(20);
      // Creates one person with randomized names.
      var person = generator.Generate();
    }
  }
  internal class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string WorkPlace { get; set; }
  }
}
```

These examples show how you can create instances of the type given to the GeneratorFactory.
#### 

======
### Install
Use the Nuget Package Manager Console and type Install-Package Sharpy

[Link](https://www.nuget.org/packages/Sharpy/) to package on Nuget.
### Dependencies:

* [NodaTime](https://github.com/nodatime/nodatime) for dates
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for deserializing JSON
