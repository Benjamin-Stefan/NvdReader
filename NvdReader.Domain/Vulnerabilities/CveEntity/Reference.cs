using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.Entity
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Reference
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("tags")]
        public string[]? Tags { get; set; }
    }

}
