public sealed record GetPropertyAttributeValuesResponse(IReadOnlyList<PropertyAttributeValueDto> Attributes);

public class GetPropertyAttributeValues : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/properties/{propertyId}/attributes", async (
      Guid propertyId,
      ISender sender,
      AttributeGroup? group) =>
    {
      var result = await sender.Send(new GetPropertyAttributeValuesQuery(propertyId, group));

      return Results.Ok(new GetPropertyAttributeValuesResponse(result.Value!.Attributes));
    })
      .WithName("GetPropertyAttributeValues")
      .Produces<GetPropertyAttributeValuesResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Get Property Attribute Values")
      .WithDescription("Returns the dynamic form for a property: every active attribute plus the value captured for it.");
  }
}
