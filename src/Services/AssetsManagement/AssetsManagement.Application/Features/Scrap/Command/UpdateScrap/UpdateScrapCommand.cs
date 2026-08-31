using FluentValidation;

public sealed record UpdateScrapCommandResult(bool IsSuccess);

public sealed record UpdateScrapCommand(Guid Id, ScrapDto Scrap) : ICommand<Result<UpdateScrapCommandResult>>;

public class UpdateScrapCommandValidator:AbstractValidator<UpdateScrapCommand>
{
  public UpdateScrapCommandValidator()
  {
    RuleFor(x => x.Id)
   .NotEmpty()
   .WithName("Id field is required");
  RuleFor(x => x.Scrap).SetValidator(new ScrapDtoValidator());
  }
}