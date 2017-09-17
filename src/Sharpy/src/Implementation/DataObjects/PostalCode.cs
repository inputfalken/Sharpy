using Newtonsoft.Json;

namespace Sharpy.Implementation.DataObjects {
    internal sealed class PostalCode {
        internal string Postalcode { get; }
        internal string Country { get; }

        [JsonConstructor]
        private PostalCode(string postalcode, string country) {
            Postalcode = postalcode;
            Country = country;
        }
    }
}