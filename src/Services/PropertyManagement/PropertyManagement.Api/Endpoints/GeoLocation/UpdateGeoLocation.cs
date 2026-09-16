public sealed record UpdateGeoLocationRequest(GeoLocationInput GeoLocation);
public sealed record UpdateGeoLocationResponse(bool IsSuccess);

public class UpdateGeoLocation : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/geo-locations/{id}", async (Guid id, UpdateGeoLocationRequest request, ISender sender) =>
    {
      var result = await sender.Send(new UpdateGeoLocationCommand(id, request.GeoLocation));

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<UpdateGeoLocationResponse>();

      return Results.Ok(response);
    })
      .WithName("UpdateGeoLocation")
      .Produces<UpdateGeoLocationResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Update Geo Location")
      .WithDescription("Update Geo Location");
  }
}
