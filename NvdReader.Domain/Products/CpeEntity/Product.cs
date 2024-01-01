using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Product
{
    [JsonPropertyName("cpe")]
    public required Cpe Cpe { get; set; }
}