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

###Example

```C#
     internal static class Program {
        public static void Main() {
            var generator = Sharpy.CreateGenerator(fetcher => new Person(
                fetcher.NameByType(NameTypes.MixedFirstNames),
                fetcher.NameByType(NameTypes.LastNames)));
                
            var persons = new List<Person>();
            for (int i = 0; i < 20; i++)
                persons.Add(generator());
        }
    }

    class Person {
        public Person(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
```
This code will populate a list with 20 different Person objects all with random names.

======
### TODO
* Deal with Mail & Phone Number
* Make into nuget package

### Dependencies:

 * [NodaTime](https://github.com/nodatime/nodatime) for dates
 * [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for deserializing JSON
