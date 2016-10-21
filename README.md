# Sharpy 1.0.3

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
      var generator = RandomGenerator.Create();
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
      var generator = RandomGenerator.Create();
      // The generator will now behave differently 
      // when calling the String method from randomizer using argument for last and first names.
      generator.Config.Name(Country.UnitedStates);

      
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)}, 20);
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
      var generator = RandomGenerator.Create();
   
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName),
          // Just pass an Class using IList or params!
          // This shows a params example.
          WorkPlace = randomizer.CustomCollection("Workplace1", "workplace2")}, 20);
    }
  }

  internal class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string WorkPlace { get; set; }
  }
}
```
#### Creating Multiple types with the same generator
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
    internal static class Program {
        public static void Main() {
      var generator = RandomGenerator.Create();   
      
      IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
          FirstName = randomizer.String(StringType.FirstName),
          LastName = randomizer.String(StringType.LastName)}, 20);
          
      // Just use the same generator and call GenerateMany!
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
#### Generating with nested IEnumerable
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
    internal static class Program {
        public static void Main() {
            var generator = RandomGenerator.Create();

            IEnumerable<Person> people = generator.GenerateMany(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName),
                //Just call GenerateMany but inside the type generated!
                Animals = generator.GenerateMany(animalRandomizer => new Animal {Age = animalRandomizer.Integer(10, 20)})
            }, 20);
        }
    }

    internal class Animal {
        public int Age { get; set; }
    }

    internal class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Animal> Animals { get; set; }
    }
}
```
#### Passing same generated result to multiple arguments.
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace Logger {
    internal static class Program {
        public static void Main() {
            var generator = RandomGenerator.Create();
            //At the moment you have to make a statement lambda.
            IEnumerable<Person> people = generator.GenerateMany(randomizer => {
                //Reference the result from the randomizer methods
                var firstName = randomizer.String(StringType.FirstName);
                var lastName = randomizer.String(StringType.LastName);

                //Use the results and pass them to the person.
                var person = new Person {
                    FirstName = firstName,
                    LastName = lastName,
                    MailAddress = randomizer.MailAdress(firstName, lastName)
                };

                return person;
            }, 20);
        }
    }

    internal class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
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
