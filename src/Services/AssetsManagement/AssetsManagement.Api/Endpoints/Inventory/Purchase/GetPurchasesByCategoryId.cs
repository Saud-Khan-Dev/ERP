
public class GetPurchasesByCategoryId : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/purchases/category/{id}", async (Guid id, ISender sender) =>
   {
     var result = await sender.Send(new GetAllPurchasesByCategoryIdQuery(id));
    //  var response = result.Value.Adapt<GetInventoryTypeResponse>();

     return Results.Ok();
   })
   .WithName("GetAllPurchasesByCategoryId")
       .Produces<GetInventoryTypeResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get AllPurchases ByCategory Id")
       .WithDescription("Get AllPurchases ByCategory Id");
  }
}