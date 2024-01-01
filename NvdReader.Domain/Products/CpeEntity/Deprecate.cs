using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Deprecate
{
    [JsonPropertyName("cpeName")]
    public string? CpeName { get; set; }
    
    [JsonPropertyName("cpeNameId")]
    public string? CpeNameId { get; set; }
}