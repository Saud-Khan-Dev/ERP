public sealed record SavePropertyAttributeValuesRequest(IReadOnlyList<AttributeValueInput> Values);
public sealed record SavePropertyAttributeValuesResponse(int SavedCount);

public class SavePropertyAttributeValues : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/properties/{propertyId}/attributes", async (
      Guid propertyId,
      SavePropertyAttributeValuesRequest request,
      ISender sender) =>
    {
      var result = await sender.Send(new SavePropertyAttributeValuesCommand(propertyId, request.Values));
      var response = result.Value.Adapt<SavePropertyAttributeValuesResponse>();

      return Results.Ok(response);
    })
      .WithName("SavePropertyAttributeValues")
      .Produces<SavePropertyAttributeValuesResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Save Property Attribute Values")
      .WithDescription("Saves the dynamic data-entry form for a property; each value is validated against its attribute's configured data type.");
  }
}
