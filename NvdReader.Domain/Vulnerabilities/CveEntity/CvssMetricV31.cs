using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CvssMetricV31
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("cvssData")]
    public CvssData? CvssData { get; set; }

    [JsonPropertyName("exploitabilityScore")]
    public float ExploitabilityScore { get; set; }

    [JsonPropertyName("impactScore")]
    public float ImpactScore { get; set; }
}