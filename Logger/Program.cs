using System;
using System.Linq;
using DataGen.Types.NameCollection;

namespace Logger {
    internal static class Program {
        private static void Main(string[] args) {
            Name.Names.Value
                .FilterBy(NameArg.Region, "southAmerica")
                .FilterBy(NameArg.Lastname)
                .ToStringFilter(name => name.ToString())
                .FilterBy(StringArg.Contains, "zo")
                .OrderBy(s => s)
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}