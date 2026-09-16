public sealed record DeactivateAttributeDefinitionRequest(bool IsActive);
public sealed record DeactivateAttributeDefinitionResponse(bool IsSuccess);

public class DeactivateAttributeDefinition : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPatch("/attribute-definitions/{id}/status", async (Guid id, DeactivateAttributeDefinitionRequest request, ISender sender) =>
    {
      var result = await sender.Send(new DeactivateAttributeDefinitionCommand(id, request.IsActive));
      var response = result.Value.Adapt<DeactivateAttributeDefinitionResponse>();

      return Results.Ok(response);
    })
      .WithName("DeactivateAttributeDefinition")
      .Produces<DeactivateAttributeDefinitionResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Activate or Deactivate Attribute Definition")
      .WithDescription("Attribute definitions are never hard-deleted, so historical values stay valid; they are deactivated instead.");
  }
}
