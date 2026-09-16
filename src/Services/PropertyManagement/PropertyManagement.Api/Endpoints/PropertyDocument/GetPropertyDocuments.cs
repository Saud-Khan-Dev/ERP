public sealed record GetPropertyDocumentsResponse(IReadOnlyList<PropertyDocumentDto> Documents);

public class GetPropertyDocuments : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/properties/{propertyId}/documents", async (
      Guid propertyId,
      ISender sender,
      AttributeGroup? relatedGroup) =>
    {
      var result = await sender.Send(new GetPropertyDocumentsQuery(propertyId, relatedGroup));

      return Results.Ok(new GetPropertyDocumentsResponse(result.Value!.Documents));
    })
      .WithName("GetPropertyDocuments")
      .Produces<GetPropertyDocumentsResponse>(StatusCodes.Status200OK)
      .WithSummary("Get Property Documents")
      .WithDescription("Get Property Documents");
  }
}
