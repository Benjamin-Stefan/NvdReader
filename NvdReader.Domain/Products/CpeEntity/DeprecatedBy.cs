using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class DeprecatedBy
{
    [JsonPropertyName("cpeName")]
    public string? CpeName { get; set; }

    [JsonPropertyName("cpeNameId")]
    public string? CpeNameId { get; set; }
}