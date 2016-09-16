using System;
using System.Linq;

namespace Sharpy.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// 
    /// </summary>
    public class PhoneNumberGenerator : Unique<string> {
        private CountryCode CountryCode { get; }
        /// <summary>
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="random"></param>
        /// <param name="unique"></param>
        internal PhoneNumberGenerator(CountryCode countryCode, Random random, bool unique = false) : base(50, random) {
            CountryCode = countryCode;
            Unique = unique;
        }


        /// <summary>
        /// 
        /// </summary>
        private bool Unique { get;  }


        /// <summary>
        ///     Will create a phone number by randoming numbers including country code
        ///     <param name="length">The length of the number</param>
        ///     <param name="preNumber">Optional number that will be used before the random numbers</param>
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
            foreach (var i in Enumerable.Range(0, length)) {
                if (i == 0) Builder.Append(CountryCode.Code).Append(preNumber);
                Builder.Append(Random.Next(10));
            }
            var str = Builder.ToString();
            Builder.Clear();
            return str;
        }

        /// <summary>
        ///     This overLoad will also randomize a phone number length within min and max length
        ///     <param name="minLength">Min length of the phone number</param>
        ///     <param name="maxLength">Max length of the phone number</param>
        ///     <param name="preNumber">Optional number that will be used before the random numbers</param>
        /// </summary>
        public string RandomNumber(int minLength, int maxLength, string preNumber = null)
            => RandomNumber(Random.Next(minLength, maxLength), preNumber);
    }
}