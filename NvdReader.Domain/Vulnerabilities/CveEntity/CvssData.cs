using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CvssData
{
    [JsonPropertyName("accessVector")]
    public string? AccessVector { get; set; }

    [JsonPropertyName("accessComplexity")]
    public string? AccessComplexity { get; set; }

    [JsonPropertyName("authentication")]
    public string? Authentication { get; set; }
        
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("vectorString")]
    public string? VectorString { get; set; }

    [JsonPropertyName("attackVector")]
    public string? AttackVector { get; set; }

    [JsonPropertyName("attackComplexity")]
    public string? AttackComplexity { get; set; }

    [JsonPropertyName("privilegesRequired")]
    public string? PrivilegesRequired { get; set; }

    [JsonPropertyName("userInteraction")]
    public string? UserInteraction { get; set; }

    [JsonPropertyName("scope")]
    public string? Scope { get; set; }

    [JsonPropertyName("confidentialityImpact")]
    public string? ConfidentialityImpact { get; set; }

    [JsonPropertyName("integrityImpact")]
    public string? IntegrityImpact { get; set; }

    [JsonPropertyName("availabilityImpact")]
    public string? AvailabilityImpact { get; set; }

    [JsonPropertyName("baseScore")]
    public float BaseScore { get; set; }

    [JsonPropertyName("baseSeverity")]
    public string? BaseSeverity { get; set; }
}