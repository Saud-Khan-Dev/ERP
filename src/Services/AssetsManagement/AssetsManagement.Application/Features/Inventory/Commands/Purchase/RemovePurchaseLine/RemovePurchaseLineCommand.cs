using FluentValidation;

public record RemovePurchaseLineCommandResult(bool IsSuccess);

public record RemovePurchaseLineCommand(Guid Id) : ICommand<Result<RemovePurchaseLineCommandResult>>;

public class RemovePurchaseLineCommandValidator : AbstractValidator<RemovePurchaseLineCommand>
{
  public RemovePurchaseLineCommandValidator()
  {
    RuleFor(x => x.Id)
     .NotEmpty()
     .WithMessage("Purchase Id is required");

  }
}