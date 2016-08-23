using System;
using System.Linq;

namespace DataGen.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    public class CountryCode : Unique<string> {
        public string Name { get; }
        public string Code { get; }

        public CountryCode(string name, string code) {
            Name = name;
            Code = code;
        }

        ///<summary>
        ///     Will create a phone number by randoming numbers including country code
        /// <param name="length">The length of the number</param>
        /// <param name="preNumber">Optional number that will be used before the random numbers</param>
        /// </summary>
        public string CreateRandomNumber(int length, string preNumber = null) {
            var attempts = 0;
            while (attempts < 50) {
                foreach (var i in Enumerable.Range(1, length)) {
                    if (i == 1)
                        Builder.Append(Code).Append(preNumber);
                    Builder.Append(HelperClass.Randomizer(0, 9));
                }
                var number = Builder.ToString();
                if (ClearValidateSave(number))
                    return number;
                attempts += 1;
            }
            throw new Exception("Could not create an unique number");
        }

        ///<summary>
        ///     This overLoad will also randomize a phone number length within min and max length
        /// <param name="minLength">Min length of the phone number</param>
        /// <param name="maxLength">Max length of the phone number</param>
        /// <param name="preNumber">Optional number that will be used before the random numbers</param>
        /// </summary>
        public string CreateRandomNumber(int minLength, int maxLength, string preNumber = null) {
            var attempts = 0;
            while (attempts < 50) {
                foreach (var i in Enumerable.Range(1, HelperClass.Randomizer(minLength, maxLength))) {
                    if (i == 1)
                        Builder.Append(Code).Append(preNumber);
                    Builder.Append(HelperClass.Randomizer(0, 9));
                }
                var number = Builder.ToString();
                if (ClearValidateSave(number))
                    return number;
                attempts += 1;
            }
            throw new Exception("Could not create an unique number");
        }
    }
}