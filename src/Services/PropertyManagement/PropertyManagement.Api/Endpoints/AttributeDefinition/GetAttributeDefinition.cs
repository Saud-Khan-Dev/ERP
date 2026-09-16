public sealed record GetAttributeDefinitionResponse(AttributeDefinitionDto AttributeDefinition);

public class GetAttributeDefinition : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/attribute-definitions/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new GetAttributeDefinitionQuery(id));
      var response = result.Value.Adapt<GetAttributeDefinitionResponse>();

      return Results.Ok(response);
    })
      .WithName("GetAttributeDefinition")
      .Produces<GetAttributeDefinitionResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Get Attribute Definition")
      .WithDescription("Get Attribute Definition");
  }
}
