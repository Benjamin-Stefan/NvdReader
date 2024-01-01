using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Description
{
    [JsonPropertyName("lang")]
    public string? Lang { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}