public sealed record CreatePropertyRequest(PropertyInput Property, GeoLocationInput? GeoLocation, Guid? GeoLocationId);
public sealed record CreatePropertyResponse(Guid Id, Guid GeoLocationId);

public class CreateProperty : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/properties", async (CreatePropertyRequest request, ISender sender) =>
    {
      var command = request.Adapt<CreatePropertyCommand>();
      var result = await sender.Send(command);

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<CreatePropertyResponse>();

      return Results.Created($"/properties/{response!.Id}", response);
    })
      .WithName("CreateProperty")
      .Produces<CreatePropertyResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Property")
      .WithDescription("Creates a property with either a new inline geo location or an existing unlinked geoLocationId.");
  }
}
