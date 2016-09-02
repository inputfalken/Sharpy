# Sharpy

The idea of this project is to let users have a source to fetch random data from.

####Features

 * LocalDate
 * Bool
 * Name
 * Int
 * Mail
 * Phone number

Mail and Phone number currently throws an exception if they fail to create an unique string.

###Examples
#### Generating
```C#
      internal static class Program {
        public static void Main() {
            var generator = Sharpy.CreateGenerator(randomizer => new Person {
                FirstName = randomizer.NameByType(NameTypes.MixedFirstNames),
                LastName = randomizer.NameByType(NameTypes.LastNames)
            });
            // creates an IEnmuerable<Person> with twenty persons. All with randomized names.
            var persons = generator.Generate(20);
            // Creates one person 
            var person = generator.Generate();
        }
    }

    class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
```
#### Configure
```C#
      internal static class Program {
        public static void Main() {
            var generator = Sharpy.CreateGenerator(randomizer => new Person {
                FirstName = randomizer.NameByType(NameTypes.MixedFirstNames),
                LastName = randomizer.NameByType(NameTypes.LastNames)
            });
            generator.Config.Names(filter => filter.ByCountry("unitedStates"));
            // creates an IEnmuerable<Person> with twenty persons.
            // All with randomized names from the United States.
            var persons = generator.Generate(20);
            // Creates one person with randomized Lastname & Firstname from the United States
            var person = generator.Generate();
        }
    }

    class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
```
This code shows how you can create instances of the type given to the CreateGenerator method.
#### 

======
### TODO
* Deal with Mail & Phone Number
* Make into nuget package

### Dependencies:

 * [NodaTime](https://github.com/nodatime/nodatime) for dates
 * [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for deserializing JSON
