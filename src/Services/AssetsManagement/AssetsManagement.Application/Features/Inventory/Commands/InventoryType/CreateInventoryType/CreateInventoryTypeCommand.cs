using FluentValidation;

public sealed record CreateInventoryResult(Guid Id);
public sealed record CreateInventoryTypeCommand(InventoryTypeDto InventoryType) : ICommand<Result<CreateCategoryResult>>;

public class CreateInventoryTypeCommandValidator:AbstractValidator<CreateInventoryTypeCommand>
{
  
}

public class InventoryTypeDtoValidator:AbstractValidator<InventoryTypeDto>
{
  public InventoryTypeDtoValidator()
  {
    
  }
}
