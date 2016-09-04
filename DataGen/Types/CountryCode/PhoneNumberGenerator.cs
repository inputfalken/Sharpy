using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using static DataGen.Types.HelperClass;

namespace DataGen.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    public class PhoneNumberGenerator : Unique<string> {
        public string Name { get; }
        public string Code { get; }
        public bool Unique { get; set; }

        public PhoneNumberGenerator(string name, string code) : base(50) {
            Name = name;
            Code = code;
        }

        ///<summary>
        ///     Will create a phone number by randoming numbers including country code
        /// <param name="length">The length of the number</param>
        /// <param name="preNumber">Optional number that will be used before the random numbers</param>
        /// </summary>
        public string RandomNumber(int length, string preNumber = null) {
            if (!Unique) return BuildString(length, preNumber);
            var attempts = 0;
            while (attempts < AttemptLimit) {
                var number = BuildString(length, preNumber);
                if (ClearValidateSave(number))
                    return number;
                attempts += 1;
            }
            throw new Exception("Could not create an unique number");
        }

        private string BuildString(int length, string preNumber) {
            foreach (var i in Range(0, length)) {
                if (i == 0) Builder.Append(Code).Append(preNumber);
                Builder.Append(SetRandomizer(10));
            }
            var str = Builder.ToString();
            Builder.Clear();
            return str;
        }

        ///<summary>
        ///     This overLoad will also randomize a phone number length within min and max length
        /// <param name="minLength">Min length of the phone number</param>
        /// <param name="maxLength">Max length of the phone number</param>
        /// <param name="preNumber">Optional number that will be used before the random numbers</param>
        /// </summary>
        public string RandomNumber(int minLength, int maxLength, string preNumber = null)
            => RandomNumber(SetRandomizer(minLength, maxLength), preNumber);
    }
}