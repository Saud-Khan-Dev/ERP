public sealed record GetPropertiesResponse(PaginatedResult<PropertyDto> Properties);

public class GetProperties : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/properties", async (
      ISender sender,
      int? pageIndex,
      int? pageSize,
      PropertyStatus? status) =>
    {
      var pagination = new PaginationRequest(pageIndex ?? 0, pageSize ?? 10);
      var result = await sender.Send(new GetPropertiesQuery(pagination, status));

      return Results.Ok(new GetPropertiesResponse(result.Value!.Properties));
    })
      .WithName("GetProperties")
      .Produces<GetPropertiesResponse>(StatusCodes.Status200OK)
      .WithSummary("Get Properties")
      .WithDescription("Returns a paginated list of properties, optionally filtered by status.");
  }
}
