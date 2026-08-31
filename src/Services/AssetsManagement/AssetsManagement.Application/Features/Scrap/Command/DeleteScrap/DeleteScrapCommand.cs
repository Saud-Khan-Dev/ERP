using FluentValidation;

public record DeleteScrapCommandResult(bool IsSuccess);

public record DeleteScrapCommand(Guid Id) : ICommand<Result<DeleteScrapCommandResult>>;

public class DeleteScrapCommandValidator : AbstractValidator<DeleteScrapCommand>
{
  public DeleteScrapCommandValidator()
  {
    RuleFor(x => x.Id)
    .NotEmpty()
    .WithName("Id field is required");
  }
}