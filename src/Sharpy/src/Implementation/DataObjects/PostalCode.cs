using Newtonsoft.Json;

namespace Sharpy.Implementation.DataObjects {
    internal sealed class PostalCode {
        [JsonConstructor]
        private PostalCode(string postalcode, string county) {
            Postalcode = postalcode;
            County = county;
        }

        [JsonProperty("postalCode")]
        internal string Postalcode { get; }

        [JsonProperty("county")]
        internal string County { get; }
    }
}