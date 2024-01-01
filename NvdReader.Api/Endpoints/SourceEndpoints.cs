using Microsoft.AspNetCore.Http.HttpResults;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.DataSources.SourceEntity;

namespace NvdReader.Api.Endpoints;

public static class SourceEndpoints
{
    public static IEndpointRouteBuilder UseSourceEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ArgumentNullException.ThrowIfNull(routeBuilder);

        routeBuilder.MapGet("/source/{sourceIdentifier}", 
                async Task<Results<Ok<Source>, NotFound>> (ISourceService service, string sourceIdentifier, CancellationToken cancellationToken) =>
                        {
                            var result = await service.GetSourceBySourceIdentifierAsync(sourceIdentifier, cancellationToken);
                            return result == null
                                ? TypedResults.NotFound()
                                : TypedResults.Ok(result);
                        })
                        .WithName("GetSource")
                        .WithOpenApi();

        return routeBuilder;
    }
}
