public sealed record DeletePropertyResponse(bool IsSuccess);

public class DeleteProperty : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/properties/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new DeletePropertyCommand(id));
      var response = result.Value.Adapt<DeletePropertyResponse>();

      return Results.Ok(response);
    })
      .WithName("DeleteProperty")
      .Produces<DeletePropertyResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Delete Property")
      .WithDescription("Deletes a property along with its geo location, attribute values and documents.");
  }
}
