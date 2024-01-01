using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Weakness
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("description")]
    public Description[]? Description { get; set; }
}