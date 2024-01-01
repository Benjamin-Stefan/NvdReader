using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Refs
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}