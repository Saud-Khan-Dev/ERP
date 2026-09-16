public sealed record DeletePropertyAttributeValueResponse(bool IsSuccess);

public class DeletePropertyAttributeValue : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/properties/{propertyId}/attributes/{attributeDefinitionId}", async (
      Guid propertyId,
      Guid attributeDefinitionId,
      ISender sender) =>
    {
      var result = await sender.Send(new DeletePropertyAttributeValueCommand(propertyId, attributeDefinitionId));
      var response = result.Value.Adapt<DeletePropertyAttributeValueResponse>();

      return Results.Ok(response);
    })
      .WithName("DeletePropertyAttributeValue")
      .Produces<DeletePropertyAttributeValueResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Clear Property Attribute Value")
      .WithDescription("Clears one captured value for a property without touching the attribute definition.");
  }
}
