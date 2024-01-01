using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveHistoryEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Change
{
    [JsonIgnore, BsonId]
    public ObjectId Id { get; set; }

    [JsonPropertyName("cveId")]
    public required string CveId { get; set; }

    [JsonPropertyName("eventName")]
    public string? EventName { get; set; }

    [JsonPropertyName("cveChangeId")]
    public required string CveChangeId { get; set; }

    [JsonPropertyName("sourceIdentifier")]
    public string? SourceIdentifier { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("details")]
    public Detail[]? Details { get; set; }
}