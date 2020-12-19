namespace Random.Extensions
{
    // TODO add overloads for this struct where you can specify inclusive or exclusive operations.
    internal readonly struct Clusivity<T>
    {
        public enum Rule
        {
            Exclusive = 0,
            Inclusive = 1
        }

        public readonly T Element { get; }
        public readonly Rule RuleSet { get; }

        public Clusivity(in T element, in Rule rule)
        {
            Element = element;
            RuleSet = rule;
        }
    }
}