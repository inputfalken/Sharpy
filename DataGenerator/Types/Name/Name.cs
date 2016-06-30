using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal abstract class Name
    {
        protected string Data { private get; set; }
        public override string ToString() => Data;
    }
}