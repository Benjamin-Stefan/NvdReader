using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Configuration
{
    [JsonPropertyName("nodes")]
    public Node[]? Nodes { get; set; }

    [JsonPropertyName("_operator")]
    public string? Operator { get; set; }
}