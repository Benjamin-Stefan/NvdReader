using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveHistoryEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CveChange
{
    [JsonPropertyName("change")]
    public required Change Change { get; set; }
}