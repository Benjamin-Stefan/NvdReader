using System.Diagnostics.CodeAnalysis;

namespace NvdReader.Domain.Vulnerabilities.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CveRequest
{
    public string? CveId { get; set; }
    
    public DateTime? LastModStartDate { get; set; }

    public DateTime? LastModEndDate { get; set; }

    public int? ResultsPerPage { get; set; }

    public int StartIndex { get; set; }

}