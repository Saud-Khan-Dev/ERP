public sealed record UpdatePropertyDocumentRequest(PropertyDocumentInput Document);
public sealed record UpdatePropertyDocumentResponse(bool IsSuccess);

public class UpdatePropertyDocument : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/property-documents/{id}", async (Guid id, UpdatePropertyDocumentRequest request, ISender sender) =>
    {
      var result = await sender.Send(new UpdatePropertyDocumentCommand(id, request.Document));
      var response = result.Value.Adapt<UpdatePropertyDocumentResponse>();

      return Results.Ok(response);
    })
      .WithName("UpdatePropertyDocument")
      .Produces<UpdatePropertyDocumentResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Update Property Document")
      .WithDescription("Updates document metadata such as type, group tag, file name or remarks.");
  }
}
