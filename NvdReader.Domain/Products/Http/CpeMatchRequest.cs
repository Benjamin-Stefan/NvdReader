using System.Diagnostics.CodeAnalysis;

namespace NvdReader.Domain.Products.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CpeMatchRequest
{
    public string? CveId  { get; set; }

    public DateTime? LastModStartDate { get; set; }

    public DateTime? LastModEndDate { get; set; }

    public string? MatchCriteriaId { get; set; }

    public string? MatchStringSearch { get; set; }

    public int? ResultsPerPage { get; set; }

    public int StartIndex { get; set; }
}