using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Node
{
    [JsonPropertyName("_operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("negate")]
    public bool Negate { get; set; }

    [JsonPropertyName("cpeMatch")]
    public CpeMatch[]? CpeMatch { get; set; }
}