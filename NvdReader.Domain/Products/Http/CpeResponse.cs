using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using NvdReader.Domain.Products.CpeEntity;

namespace NvdReader.Domain.Products.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CpeResponse : BaseResponse
{
    [JsonPropertyName("products")]
    public required Product[] Products { get; set; }
}