public sealed record CreateGeoLocationRequest(GeoLocationInput GeoLocation);
public sealed record CreateGeoLocationResponse(Guid Id);

public class CreateGeoLocation : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/geo-locations", async (CreateGeoLocationRequest request, ISender sender) =>
    {
      var result = await sender.Send(new CreateGeoLocationCommand(request.GeoLocation));

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<CreateGeoLocationResponse>();

      return Results.Created($"/geo-locations/{response!.Id}", response);
    })
      .WithName("CreateGeoLocation")
      .Produces<CreateGeoLocationResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Geo Location")
      .WithDescription("Registers a geo location that can later be linked when creating a property.");
  }
}
