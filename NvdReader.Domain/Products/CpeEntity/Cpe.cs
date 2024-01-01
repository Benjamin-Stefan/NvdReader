using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Cpe
{
    [JsonIgnore, BsonId]
    public ObjectId  Id { get; set; }

    [JsonPropertyName("deprecated")]
    public bool Deprecated { get; set; }

    [JsonPropertyName("cpeName")]
    public string? CpeName { get; set; }

    [JsonPropertyName("cpeNameId")]
    public required string CpeNameId { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime LastModified { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("titles")]
    public Titles[]? Titles { get; set; }

    [JsonPropertyName("deprecates")]
    public Deprecate[]? Deprecates { get; set; }

    [JsonPropertyName("deprecatedBy")]
    public DeprecatedBy[]? DeprecatedBy { get; set; }

    [JsonPropertyName("refs")]
    public Refs[]? Refs { get; set; }
}