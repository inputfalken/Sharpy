using Sharpy.Enums;

namespace Sharpy {
    /// <summary>
    ///     Contains Pre-Configured generators.
    /// </summary>
    public static class PreConfigured {
        private static Generator<string> GenerateNames { get; } = new Generator<string>(
            randomizer => $"{randomizer.String(Type.MixedFirstName)} {randomizer.String(Type.LastName)}");

        /// <summary>
        ///     Generates a formated string containing First name, space followed by a Last name.
        /// </summary>
        /// <returns></returns>
        public static string Name() => GenerateNames.Generate();
    }
}