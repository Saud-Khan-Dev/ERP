public sealed record CreateAttributeDefinitionRequest(CreateAttributeDefinitionInput AttributeDefinition);
public sealed record CreateAttributeDefinitionResponse(Guid Id);

public class CreateAttributeDefinition : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/attribute-definitions", async (CreateAttributeDefinitionRequest request, ISender sender) =>
    {
      var command = request.Adapt<CreateAttributeDefinitionCommand>();
      var result = await sender.Send(command);

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<CreateAttributeDefinitionResponse>();

      return Results.Created($"/attribute-definitions/{response!.Id}", response);
    })
      .WithName("CreateAttributeDefinition")
      .Produces<CreateAttributeDefinitionResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Attribute Definition")
      .WithDescription("Defines a new dynamic attribute (name + data type) for a property attribute group.");
  }
}
