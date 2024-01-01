using Microsoft.AspNetCore.Http.HttpResults;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Vulnerabilities.CveEntity;
using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;

namespace NvdReader.Api.Endpoints;

public static class CveEndpoints
{
    public static IEndpointRouteBuilder UseCveEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ArgumentNullException.ThrowIfNull(routeBuilder);

        routeBuilder.MapGet("/cve/{cveId}",
                async Task<Results<Ok<Cve>, NotFound>> (ICveService service, string cveId, CancellationToken cancellationToken) =>
                        {
                            var result = await service.GetCveByIdAsync(cveId, cancellationToken);
                            return result == null
                                ? TypedResults.NotFound()
                                : TypedResults.Ok(result);
                        })
                        .WithName("GetCve")
                        .WithOpenApi();

        routeBuilder.MapGet("/cve/{cveId}/changes",
            async Task<Results<Ok<List<Change>>, NotFound>> (ICveService service, string cveId, CancellationToken cancellationToken) =>
                    {
                        var result = await service.GetChangeByCveIdAsync(cveId, cancellationToken);

                        return result == null
                            ? TypedResults.NotFound()
                            : TypedResults.Ok(result);
                    })
                    .WithName("GetCveChanges")
                    .WithOpenApi();

        return routeBuilder;
    }
}
