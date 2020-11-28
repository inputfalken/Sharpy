using System.Text.Json.Serialization;
using Sharpy.Builder.Enums;

namespace Sharpy.Builder.Implementation.DataObjects
{
    /// <summary>
    ///     The data structure used by <see cref="NameByOrigin" />.
    /// </summary>
    internal sealed class NameModel
    {
        [JsonPropertyName("type")]
        public NameType Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("region")]
        public Origin Region { get; set; }

        [JsonPropertyName("country")]
        public Origin Country { get; set; }
    }
}