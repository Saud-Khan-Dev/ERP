public sealed record DeleteGeoLocationResponse(bool IsSuccess);

public class DeleteGeoLocation : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/geo-locations/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new DeleteGeoLocationCommand(id));

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<DeleteGeoLocationResponse>();

      return Results.Ok(response);
    })
      .WithName("DeleteGeoLocation")
      .Produces<DeleteGeoLocationResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Delete Geo Location")
      .WithDescription("Deletes an unlinked geo location; one attached to a property is rejected.");
  }
}
