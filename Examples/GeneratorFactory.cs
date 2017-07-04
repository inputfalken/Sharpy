#region Decrementer
var decrementer = Generator.Factory.Decrementer(0);
decrementer.Generate(); // 1
decrementer.Generate(); // 2
decrementer.Generate(); // 3
#endregion

#region Incrementer
var incrementer = Generator.Factory.Incrementer(0);
incrementer.Generate(); // 1
incrementer.Generate(); // 2
incrementer.Generate(); // 3
#endregion

#region Guid
var guid = Generator.Factory.Guid();
guid.Generate(); // New Guid
guid.Generate(); // New Guid
guid.Generate(); // New Guid
#endregion

#region RandomizerOneArg
var randomizer = Generator.Factory.Randomizer();
randomizer.Generate(); // Randomized number >= 0 and &lt; int.MaxValue
randomizer.Generate(); // Randomized number >= 0 and &lt; int.MaxValue
randomizer.Generate(); // Randomized number >= 0 and &lt; int.MaxValue
#endregion

#region RandomizerTwoArg
var randomizer = Generator.Factory.Randomizer(10);
randomizer.Generate() // Randomized number >= 0 and &lt; 10
randomizer.Generate() // Randomized number >= 0 and &lt; 10
randomizer.Generate() // Randomized number >= 0 and &lt; 10
#endregion

#region RandomizerThreeArg
var randomizer = Generator.Factory.Randomizer(10, 100);
randomizer.Generate() // Randomized number >= 10 and &lt; 100
randomizer.Generate() // Randomized number >= 10 and &lt; 100
randomizer.Generate() // Randomized number >= 10 and &lt; 100
#endregion
