public sealed record GetGeoLocationsResponse(PaginatedResult<GeoLocationDto> GeoLocations);

public class GetGeoLocations : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/geo-locations", async (
      ISender sender,
      int? pageIndex,
      int? pageSize,
      bool? unlinkedOnly) =>
    {
      var pagination = new PaginationRequest(pageIndex ?? 0, pageSize ?? 10);
      var result = await sender.Send(new GetGeoLocationsQuery(pagination, unlinkedOnly ?? false));

      return Results.Ok(new GetGeoLocationsResponse(result.Value!.GeoLocations));
    })
      .WithName("GetGeoLocations")
      .Produces<GetGeoLocationsResponse>(StatusCodes.Status200OK)
      .WithSummary("Get Geo Locations")
      .WithDescription("Paginated geo locations; unlinkedOnly=true returns those not yet attached to a property.");
  }
}
