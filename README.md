# Sharpy 1.1.0

The idea of this project is to let users have a source to fetch random data from.

###Examples
#### Generating
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace ConsoleApp {
    internal static class Program {
        public static void Main() {
            // First argument is the instructions on what will be generated, 
            // second argument is the Count of the IEnumerable.
            IEnumerable<Person> people = RandomGenerator.GenerateEnumerable(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName)
            }, 20);
            // Creates one person with randomized names.
            Person person = RandomGenerator.GenerateInstance(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName)
            });
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

namespace ConsoleApp {
    internal static class Program {
        public static void Main() {
            // The generator will now behave differently 
            // when calling the String method from randomizer using argument for last and first names.
            RandomGenerator.Configurement.Name(Country.UnitedStates);

            IEnumerable<Person> people = RandomGenerator.GenerateEnumerable(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName)
            }, 20);
        }

        internal class Person {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
```
#### Supplying your own collection
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace ConsoleApp {
    internal static class Program {
        public static void Main() {
            IEnumerable<Person> people = RandomGenerator.GenerateEnumerable(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName),
                // Just pass an Class using IList or params!
                // This shows a params example.
                WorkPlace = randomizer.CustomCollection("Workplace1", "workplace2")
            }, 20);
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

namespace ConsoleApp {
    internal static class Program {
        public static void Main() {
            IEnumerable<Person> people = RandomGenerator.GenerateEnumerable(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName)
            }, 20);

            // Just use the same generator and call GenerateMany!
            IEnumerable<Animal> animals = RandomGenerator.GenerateEnumerable(randomizer => new Animal {
                Age = randomizer.Integer(10, 50)
            }, 20);
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

namespace ConsoleApp {
    internal static class Program {
        public static void Main() {
            IEnumerable<Person> people = RandomGenerator.GenerateEnumerable(randomizer => new Person {
                FirstName = randomizer.String(StringType.FirstName),
                LastName = randomizer.String(StringType.LastName),
                //Just call GenerateMany but inside the type generated!
                Animals =
                    RandomGenerator.GenerateEnumerable(
                        animalRandomizer => new Animal {Age = animalRandomizer.Integer(10, 20)})
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

namespace ConsoleApp {
    internal static class Program {
        public static void Main() {
            //At the moment you have to make a statement lambda.
            IEnumerable<Person> people = RandomGenerator.GenerateEnumerable(randomizer => {
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
These examples show how you can use the RandomGenerator static method & properties.

You could also get RandomGenerator instances by calling the static method Create in the RandomGenerator class.
Which works the same but got different names for Generating(Generate & GenerateMany).
####

======
### Install
Use the Nuget Package Manager Console and type Install-Package Sharpy

[Link](https://www.nuget.org/packages/Sharpy/) to package on Nuget.
### Dependencies:

* [NodaTime](https://github.com/nodatime/nodatime) for dates
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for deserializing JSON
