public sealed record GetGeoLocationResponse(GeoLocationDto GeoLocation);

public class GetGeoLocation : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/geo-locations/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new GetGeoLocationQuery(id));
      var response = result.Value.Adapt<GetGeoLocationResponse>();

      return Results.Ok(response);
    })
      .WithName("GetGeoLocation")
      .Produces<GetGeoLocationResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Get Geo Location")
      .WithDescription("Get Geo Location");
  }
}
