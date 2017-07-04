#region Generator.Cast
ArrayList list = new ArrayList() {1, 2, 3};
// Casts each object to int.
IGenerator<int> castedGenerator = Generator.CirkularSequence(list)
                .Cast<int>();
#endregion

#region Generator.Where
// Creates a generator whos generations will be divideable by 2.
IGenerator<int> filteredGenerator = Generator.Factory
                .Incrementer(0)
                .Where(i => i % 2 == 0);
#endregion

#region Generator.Do
// Each generation will invoke method Console.WriteLine with the incrementation as input.
IGenerator<int> generator = Generator.Factory
                .Incrementer(0)
                .Do(Console.WriteLine);
#endregion

#region Generator.Select
// Creates generator whos incrementation will be doubled.
IGenerator<int> sel = Generator.Factory
                .Incrementer(0)
                .Select(i => i * 2);
#endregion

#region Generator.Select.Counter

#endregion

#region Generator.Take
// Creates an IEnumerable<int> whos numbers are randomized.
IEnumerable<int> enumerable = Generator.Factory
                 .Randomizer(100, 1000)
                 .Take(100);
#endregion

#region Generator.SelectMany

#endregion

#region Generator.SelectMany.ResultSelector

#endregion

#region Generator.Zip
IGenerator<int> incrementer = Generator.Factory.Incrementer(0);
IGenerator<int> randomizer = Generator.Factory.Randomizer(10, 100);
// Merge randomizer with incrementer and add them up
IGenerator<int> zip = Incrementer.Zip(randomizer, (i, r) => i + r);
#endregion

#region Generator.ToDictionary
// Creates a Dictionary whos keys is the incrementation value and the value is the lenght of the incrementation string.
Dictionary<int,int> dictionary = Generator.Factory
                    .Incrementer(0)
                    .ToDictionary(20, ks => ks, vs => vs.ToString().Length);
#endregion

#region Generator.ToList
// Creates a list with 20 generations from Incrementer.
List<int> list = Generator.Factory
          .Incrementer(0)
          .ToList(20);
#endregion

#region Generator.ToArray
// Creates an array with 20 generations from Incrementer.
int[] array = Generator.Factory
      .Incrementer(0)
      .ToArray(20);
#endregion
