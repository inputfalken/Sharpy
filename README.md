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
      internal static class Program {
        public static void Main() {
            var generator = Sharpy.CreateGenerator(randomizer => new Person {
                FirstName = randomizer.NameByType(NameTypes.MixedFirstNames),
                LastName = randomizer.NameByType(NameTypes.LastNames)
            });
            // Creates an IEnumerable<Person> with twenty persons. All with randomized names.
            var persons = generator.Generate(20);
            // Creates one person with randomized names.
            var person = generator.Generate();
        }
    }

    class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
```
#### Configure & Generating
```C#
      internal static class Program {
        public static void Main() {
            var generator = Sharpy.CreateGenerator(randomizer => new Person {
                FirstName = randomizer.NameByType(NameTypes.MixedFirstNames),
                LastName = randomizer.NameByType(NameTypes.LastNames)
            });
            // Applies a filter to give common names from the United States
            generator.Config.Names(filter => filter.ByCountry(Country.UnitedStates));
            // Creates an IEnumerable<Person> with twenty persons.
            // All with randomized names from the United States.
            var persons = generator.Generate(20);
            // Creates one person with randomized names from the United States
            var person = generator.Generate();
        }
    }

    class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
```
These examples show how you can create instances of the type given to the CreateGenerator method.
#### 

======
### TODO
* Make into nuget package

### Dependencies:

 * [NodaTime](https://github.com/nodatime/nodatime) for dates
 * [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for deserializing JSON
