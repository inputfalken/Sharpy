using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes unique <see cref="string" /> representing phone numbers by <see cref="Random" />.
    /// </summary>
    public class UniquePhoneNumberRandomizer : UniqueRandomizer<int>, IPhoneNumberProvider {
        private (int, int) _numberByLengthState = (0, 0);

        /// <summary>
        ///     Creates a <see cref="UniquePhoneNumberRandomizer" />.
        /// </summary>
        public UniquePhoneNumberRandomizer(Random random) : base(random) { }

        /// <inheritdoc />
        public string PhoneNumber() => PhoneNumber(8);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string" /> with its length equal to the number given to argument
        ///         <paramref name="length" />.
        ///     </para>
        /// </summary>
        /// <param name="length">The length of the <see cref="string" /> returned.</param>
        /// <returns>
        ///     A <see cref="string" /> with numbers with its length equal to the argument <paramref name="length" />.
        /// </returns>
        /// <exception cref="Exception">Reached maximum amount of combinations for the argument <paramref name="length" />.</exception>
        public string PhoneNumber(int length) {
            //If _numberByLenghtState has changed
            if (_numberByLengthState.Item1 != length)
                _numberByLengthState = (length, (int) Math.Pow(10, length) - 1);
            var res = RandomNumber(0, _numberByLengthState.Item2, true);
            if (res == -1)
                throw new Exception($"Reached maximum amount of combinations for the argument '{nameof(length)}'.");

            var number = res.ToString();
            return number.Length != length
                ? number.Prefix(length - number.Length)
                : number;
        }

        private int RandomNumber(int min, int max, bool unique = false) {
            var next = Random.Next(min, max);
            return unique ? CreateUniqueNumber(next, min, max) : next;
        }

        private int CreateUniqueNumber(int number, int min, int max) {
            var resets = 0;
            while (HashSet.Contains(number))
                if (number < max) {
                    number++;
                }
                else {
                    number = min;
                    if (resets++ == 2) return -1;
                }
            HashSet.Add(number);
            return number;
        }
    }
}