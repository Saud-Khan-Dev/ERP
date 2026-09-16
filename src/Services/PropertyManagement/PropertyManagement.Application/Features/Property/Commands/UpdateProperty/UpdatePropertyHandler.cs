using Microsoft.EntityFrameworkCore;

public class UpdatePropertyHandler(IApplicationDbContext context)
  : ICommandHandler<UpdatePropertyCommand, Result<UpdatePropertyCommandResult>>
{
  public async Task<Result<UpdatePropertyCommandResult>> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(command.Id);
    var property = await context.Properties
        .FirstOrDefaultAsync(p => p.Id == propertyId, cancellationToken);

    if (property is null)
      throw new PropertyNotFoundException($"Property {command.Id} was not found.");

    var propertyCode = Code.Of(command.Property.PropertyCode);

    var codeAlreadyUsed = await context.Properties
        .AnyAsync(p => p.PropertyCode == propertyCode && p.Id != propertyId, cancellationToken);

    if (codeAlreadyUsed)
      return Result<UpdatePropertyCommandResult>.Failure($"A property with code {propertyCode.Value} already exists.");

    property.Update(
      propertyCode: propertyCode,
      propertyName: Name.Of(command.Property.PropertyName),
      propertyType: command.Property.PropertyType,
      ownershipType: command.Property.OwnershipType,
      areaSqFt: command.Property.AreaSqFt,
      coveredAreaSqFt: command.Property.CoveredAreaSqFt,
      landAreaSqFt: command.Property.LandAreaSqFt,
      status: command.Property.Status
    );

    await context.SaveChangesAsync(cancellationToken);

    return Result<UpdatePropertyCommandResult>.Success(new UpdatePropertyCommandResult(true));
  }
}
