using Microsoft.AspNetCore.Http.HttpResults;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Products.CpeEntity;
using NvdReader.Domain.Products.CpeMatchEntity;

namespace NvdReader.Api.Endpoints;

public static class CpeEndpoints
{
    public static IEndpointRouteBuilder UseCpeEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ArgumentNullException.ThrowIfNull(routeBuilder);

        routeBuilder.MapGet("/cpe/{cpeNameId}", 
                async Task<Results<Ok<List<Cpe>>, NotFound>> (ICpeService service, string cpeNameId, CancellationToken cancellationToken) =>
                        {
                            var result = await service.GetCpeByNameAsync(cpeNameId, cancellationToken);
                            return result == null
                                ? TypedResults.NotFound()
                                : TypedResults.Ok(result);
                        })
                        .WithName("GetCpe")
                        .WithOpenApi();

        routeBuilder.MapGet("/cpeMatch/{matchCriteriaId}", 
                async Task<Results<Ok<List<MatchString>>, NotFound>> (ICpeService service, string matchCriteriaId, CancellationToken cancellationToken) =>
                        {
                            var result = await service.GetCpeMatchByMatchCriteriaAsync(matchCriteriaId, cancellationToken);
                            return result == null
                                ? TypedResults.NotFound()
                                : TypedResults.Ok(result);
                        })
                        .WithName("GetCpeMatchForMatchCriteriaId")
                        .WithOpenApi();

        return routeBuilder;
    }
}