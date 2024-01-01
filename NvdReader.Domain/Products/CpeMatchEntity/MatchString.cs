using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeMatchEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class MatchString
{
    [JsonIgnore, BsonId]
    public ObjectId  Id { get; set; }

    [JsonPropertyName("matchCriteriaId")]
    public required string MatchCriteriaId { get; set; }

    [JsonPropertyName("criteria")]
    public string? Criteria { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime LastModified { get; set; }

    [JsonPropertyName("cpeLastModified")]
    public DateTime CpeLastModified { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("matches")]
    public Match[]? Matches { get; set; }

    [JsonPropertyName("versionEndIncluding")]
    public string? VersionEndIncluding { get; set; }
}