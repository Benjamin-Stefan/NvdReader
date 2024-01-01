using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using NvdReader.Domain.Vulnerabilities.Entity;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Cve
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("sourceIdentifier")]
    public string? SourceIdentifier { get; set; }

    [JsonPropertyName("published")]
    public DateTime Published { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime LastModified { get; set; }

    [JsonPropertyName("vulnStatus")]
    public string? VulnStatus { get; set; }

    [JsonPropertyName("descriptions")]
    public Description[]? Descriptions { get; set; }

    [JsonPropertyName("metrics")]
    public Metrics? Metrics { get; set; }

    [JsonPropertyName("weaknesses")]
    public Weakness[]? Weaknesses { get; set; }

    [JsonPropertyName("configurations")]
    public Configuration[]? Configurations { get; set; }

    [JsonPropertyName("references")]
    public Reference[]? References { get; set; }

    [JsonPropertyName("evaluatorSolution")]
    public string? EvaluatorSolution { get; set; }

    [JsonPropertyName("evaluatorImpact")]
    public string? EvaluatorImpact { get; set; }

    [JsonPropertyName("vendorComments")]
    public VendorComment[]? VendorComments { get; set; }

    [JsonPropertyName("evaluatorComment")]
    public string? EvaluatorComment { get; set; }
}