public sealed record GetPropertyDocumentResponse(PropertyDocumentDto Document);

public class GetPropertyDocument : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/property-documents/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new GetPropertyDocumentQuery(id));
      var response = result.Value.Adapt<GetPropertyDocumentResponse>();

      return Results.Ok(response);
    })
      .WithName("GetPropertyDocument")
      .Produces<GetPropertyDocumentResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Get Property Document")
      .WithDescription("Get Property Document");
  }
}
