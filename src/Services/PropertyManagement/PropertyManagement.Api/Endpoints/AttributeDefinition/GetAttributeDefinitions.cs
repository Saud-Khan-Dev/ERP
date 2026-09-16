public sealed record GetAttributeDefinitionsResponse(IReadOnlyList<AttributeDefinitionDto> AttributeDefinitions);

public class GetAttributeDefinitions : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/attribute-definitions", async (
      ISender sender,
      AttributeGroup? group,
      bool? includeInactive) =>
    {
      var result = await sender.Send(new GetAttributeDefinitionsQuery(group, includeInactive ?? false));

      return Results.Ok(new GetAttributeDefinitionsResponse(result.Value!.AttributeDefinitions));
    })
      .WithName("GetAttributeDefinitions")
      .Produces<GetAttributeDefinitionsResponse>(StatusCodes.Status200OK)
      .WithSummary("Get Attribute Definitions")
      .WithDescription("Lists configured attributes; pass group to get one data-entry form, omit it for all groups.");
  }
}
