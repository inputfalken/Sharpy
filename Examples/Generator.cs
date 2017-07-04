#region Generator.ToArray
// Creates an array with 20 generations from Incrementer.
int[] numbers = Generator.Factory
                              .Incrementer(0)
                              .ToArray(20);
#endregion
