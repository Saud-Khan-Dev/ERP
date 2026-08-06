using Carter;
using Mapster;
using MediatR;

public record DeletecategoryResponse(bool IsSuccess);

public class DeleteCategory : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/categories/{Id}", async (Guid Id, ISender sender) =>
     {
       var result = await sender.Send(new DeleteCategoryCommand(Id));
       var response = result.Value.Adapt<DeletecategoryResponse>();
       return Results.Ok(response);
     })
         .WithName("DeleteCategory")
         .Produces<DeletecategoryResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Delete Category")
         .WithDescription("Delete Category");
  }
}