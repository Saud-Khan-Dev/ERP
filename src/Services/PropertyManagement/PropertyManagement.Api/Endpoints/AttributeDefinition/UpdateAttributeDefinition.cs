public sealed record UpdateAttributeDefinitionRequest(UpdateAttributeDefinitionInput AttributeDefinition);
public sealed record UpdateAttributeDefinitionResponse(bool IsSuccess);

public class UpdateAttributeDefinition : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/attribute-definitions/{id}", async (Guid id, UpdateAttributeDefinitionRequest request, ISender sender) =>
    {
      var result = await sender.Send(new UpdateAttributeDefinitionCommand(id, request.AttributeDefinition));

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<UpdateAttributeDefinitionResponse>();

      return Results.Ok(response);
    })
      .WithName("UpdateAttributeDefinition")
      .Produces<UpdateAttributeDefinitionResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Update Attribute Definition")
      .WithDescription("Update Attribute Definition");
  }
}
