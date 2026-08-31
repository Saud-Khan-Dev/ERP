using FluentValidation;

public sealed record CreateScrapCommandResult(Guid Id);
public sealed record CreateScrapCommand(ScrapDto Scrap) : ICommand<Result<CreateScrapCommandResult>>;


public class CreateScrapCommandValidator : AbstractValidator<CreateScrapCommand>
{

}

public class ScrapDtoValidator : AbstractValidator<ScrapDto>
{
  public ScrapDtoValidator()
  {
    RuleFor(x => x.InventoryItemId).NotEmpty().NotNull()
    .WithMessage("InventoryItemId is required");
    RuleFor(x => x.Price.Amount).GreaterThan(0)
    .WithMessage("Enter a valid field");
    RuleFor(x => x.Price.Currency.Code).NotNull().NotEmpty()
  .WithMessage("Enter a currency type");

    RuleFor(x => x.Description)
    .NotEmpty()
    .NotNull()
    .WithMessage("Description is required");
  }
}
