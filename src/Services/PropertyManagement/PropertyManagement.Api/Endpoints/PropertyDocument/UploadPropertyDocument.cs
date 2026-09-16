public sealed record UploadPropertyDocumentRequest(PropertyDocumentInput Document);
public sealed record UploadPropertyDocumentResponse(Guid Id);

public class UploadPropertyDocument : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/properties/{propertyId}/documents", async (
      Guid propertyId,
      UploadPropertyDocumentRequest request,
      ISender sender) =>
    {
      var result = await sender.Send(new UploadPropertyDocumentCommand(propertyId, request.Document));
      var response = result.Value.Adapt<UploadPropertyDocumentResponse>();

      return Results.Created($"/properties/{propertyId}/documents/{response!.Id}", response);
    })
      .WithName("UploadPropertyDocument")
      .Produces<UploadPropertyDocumentResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Link Property Document")
      .WithDescription("Links an uploaded file to a property, optionally tagged to an attribute group.");
  }
}
