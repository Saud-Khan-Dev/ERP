using FluentValidation;

public sealed record GetScrapQueryResult(ScrapDto Scrap);

public sealed record GetScrapQuery(Guid Id) : IQuery<Result<GetScrapQueryResult>>;

public class GetScrapQueryValidator:AbstractValidator<GetScrapQuery>
{
  public GetScrapQueryValidator()
  {
    RuleFor(x => x.Id)
   .NotEmpty()
   .WithName("Id field is required");
  }
}