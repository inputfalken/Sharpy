# Sharpy 1.0.0

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
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
  internal static class Program {
    public static void Main() {
      // Creates a Generator which will randomize the data 
      // to the to type returned in by the methods from the generator.
      var generator = Factory.RandomGenerator();
      // First argument is the instructions on what will be generated, 
      // second argument is the Count of the IEnumerable.
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)}, 20);
      // Creates one person with randomized names.
      Person person = generator.Generate(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)});
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
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
  internal static class Program {
    public static void Main() {
      var generator = Factory.RandomGenerator();
      // The generator will now behave differently 
      // when calling the String method from randomizer using argument for last and first names.
      generator.Config.Name(Country.UnitedStates);

      
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)}, 20);
          
      Person person = generator.Generate(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)});
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
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
  internal static class Program {
    public static void Main() {
      var generator = Factory.RandomGenerator();
   
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName),
          // CustomCollection method can take params, array and list as argument.
          // This shows a params example.
          WorkPlace = randomizer.CustomCollection("Workplace1", "workplace2")}, 20);
          
      Person person = generator.Generate(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName),
          WorkPlace = randomizer.CustomCollection("Workplace1", "workplace2")});
    }
  }

  internal class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string WorkPlace { get; set; }
  }
}
```
#### Creating Multiple types using same generator
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
    internal static class Program {
        public static void Main() {
      var generator = Factory.RandomGenerator();   
      
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)}, 20);
          
      // Creates a IEnumerable<Animal> containing 20 Animals using same generator.
      IEnumerable<Animal> animals = generator.GenerateMany(randomizer => new Animal {
          Age = randomizer.Integer(10, 50)}, 20);
        }
    }

    internal class Animal {
        public int Age { get; set; }
    }

    internal class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
