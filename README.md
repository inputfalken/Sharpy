# Sharpy 2.0.0

The idea of this project is to let users have a source to fetch random data from.

###Examples

#### Generating
```C#
using System.Collections.Generic;
using Sharpy;
using Sharpy.Enums;

namespace ConsoleApp {
        private static Generator Generator { get; } = new Generator();

        public static void Main() {
            // First argument is the instructions on what will be generated, 
            // second argument is the Count of the IEnumerable.
            IEnumerable<Person> people = Generator.GenerateSequence(generator => new Person {
                FirstName = generator.FirstName(),
                LastName = generator.LastName()
            }, 20);
            // Creates one person with randomized names.
            Person person = Generator.Generate(generator => new Person {
                FirstName = generator.FirstName(),
                LastName = generator.LastName()
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
        private static readonly Generator Generator = new Generator(new Configurement {
            // Limits the FirstName and LastName method to common names in the unitedstates.
            NameProvider = new NameByOrigin(Origin.UnitedStates)
        });

        private static void Main() {
            IEnumerable<Person> people = Generator.GenerateSequence(generator => new Person {
                FirstName = generator.FirstName(),
                LastName = generator.LastName()
            }, 20);
        }

        private class Person {
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
        private static Generator Generator { get; } = new Generator();

        public static void Main() {
            IEnumerable<Person> people = Generator.GenerateSequence(generator => new Person {
                FirstName = generator.FirstName(),
                LastName = generator.LastName(),
                // Just pass an Class using IList or params!
                // This shows a params example.
                WorkPlace = generator.Params("Workplace1", "workplace2")
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
        private static Generator Generator { get; } = new Generator();

        public static void Main() {
            IEnumerable<Person> people = Generator.GenerateSequence(generator => new Person {
                FirstName = generator.FirstName(),
                LastName = generator.LastName()
            }, 20);

            // Just use the same generator and call GenerateSequence!
            IEnumerable<Animal> animals = Generator.GenerateSequence(generator => new Animal {
                Age = generator.Integer(10, 50)
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
        private static Generator Generator { get; } = new Generator();

        public static void Main() {
            IEnumerable<Person> people = Generator.GenerateSequence(generator => new Person {
                FirstName = generator.FirstName(),
                LastName = generator.LastName(),
                //Just call GenerateSequence but inside the type generated!
                Animals =
                    Generator.GenerateSequence(animalgenerator => new Animal {Age = animalgenerator.Integer(10, 20)})
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

namespace ConsoleApp {
    internal static class Program {
        private static Generator Generator { get; } = new Generator();

        public static void Main() {
            //At the moment you have to make a statement lambda.
            IEnumerable<Person> people = Generator.GenerateSequence(generator => {
                //Reference the result from the generator methods
                var firstName = generator.FirstName();
                var lastName = generator.LastName();

                //Use the results and pass them to the person.
                var person = new Person {
                    FirstName = firstName,
                    LastName = lastName,
                    MailAddress = generator.MailAddress(firstName, lastName)
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

#### Using anonymous types
```C#
using Sharpy;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            var generator = new Generator();
            // Return a anonymous type here.
            generator.Generate(x => new {
                FirstName = x.FirstName(),
                Integer = x.Integer()
            });
        }
    }
}
```

#### Adding your own methods
```C#
using Sharpy;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            var myGenerator = new MyGenerator();
            myGenerator.Generate(g => new MyClass {
                ResultFromMyStringMethod = g.MyStringMethod(),
                RandomNumber = g.Integer()
            });
        }
    }

    class MyClass {
        public string ResultFromMyStringMethod { get; set; }
        public int RandomNumber { get; set; }
    }

    // Derive from Generator in order to get all methods from it.
    class MyGenerator : Generator {
        public string MyStringMethod() {
            return "";
        }
    }
}

```
These examples show how to use my implementation of IGenerator.

### Install
Use the Nuget Package Manager Console and type Install-Package Sharpy

[Link](https://www.nuget.org/packages/Sharpy/) to package on Nuget.
### Dependencies:

* [NodaTime](https://github.com/nodatime/nodatime) for dates
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for deserializing JSON
