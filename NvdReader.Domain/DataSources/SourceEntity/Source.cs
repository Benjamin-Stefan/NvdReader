using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.DataSources.SourceEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Source
{
    [JsonIgnore, BsonId]
    public ObjectId  Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }

    [JsonPropertyName("sourceIdentifiers")]
    public required string[] SourceIdentifiers { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime LastModified { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("v3AcceptanceLevel")]
    public V3AcceptanceLevel? V3AcceptanceLevel { get; set; }

    [JsonPropertyName("cweAcceptanceLevel")]
    public CweAcceptanceLevel? CweAcceptanceLevel { get; set; }

    [JsonPropertyName("v2AcceptanceLevel")]
    public V2AcceptanceLevel? V2AcceptanceLevel { get; set; }
}