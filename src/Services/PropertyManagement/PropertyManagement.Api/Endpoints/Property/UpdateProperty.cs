public sealed record UpdatePropertyRequest(PropertyInput Property);
public sealed record UpdatePropertyResponse(bool IsSuccess);

public class UpdateProperty : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/properties/{id}", async (Guid id, UpdatePropertyRequest request, ISender sender) =>
    {
      var result = await sender.Send(new UpdatePropertyCommand(id, request.Property));

      if (!result.IsSuccess)
        return Results.BadRequest(new { Message = result.Message });

      var response = result.Value.Adapt<UpdatePropertyResponse>();

      return Results.Ok(response);
    })
      .WithName("UpdateProperty")
      .Produces<UpdatePropertyResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Update Property")
      .WithDescription("Update Property");
  }
}
