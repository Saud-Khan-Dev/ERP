public sealed record GetPropertyResponse(PropertyDto Property);

public class GetProperty : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/properties/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new GetPropertyQuery(id));
      var response = result.Value.Adapt<GetPropertyResponse>();

      return Results.Ok(response);
    })
      .WithName("GetProperty")
      .Produces<GetPropertyResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Get Property")
      .WithDescription("Get Property");
  }
}
