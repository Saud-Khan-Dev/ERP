public sealed record DeletePropertyDocumentResponse(bool IsSuccess);

public class DeletePropertyDocument : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/property-documents/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new DeletePropertyDocumentCommand(id));
      var response = result.Value.Adapt<DeletePropertyDocumentResponse>();

      return Results.Ok(response);
    })
      .WithName("DeletePropertyDocument")
      .Produces<DeletePropertyDocumentResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Delete Property Document")
      .WithDescription("Delete Property Document");
  }
}
