using System.Globalization;

public class AttributeDefinition : Aggregate<AttributeDefinitionId>
{
  private const int MaxTextLength = 4000;

  public AttributeGroup Group { get; private set; }
  public Code Code { get; private set; } = default!;
  public Name Label { get; private set; } = default!;
  public AttributeDataType DataType { get; private set; }
  public bool IsRequired { get; private set; }
  public string? OptionsCsv { get; private set; }
  public string? DefaultValue { get; private set; }
  public int DisplayOrder { get; private set; }
  public bool IsActive { get; private set; }

  public IReadOnlyList<string> Options =>
      string.IsNullOrWhiteSpace(OptionsCsv)
          ? Array.Empty<string>()
          : OptionsCsv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

  public static AttributeDefinition Create(
      AttributeDefinitionId attributeDefinitionId,
      AttributeGroup group,
      Code code,
      Name label,
      AttributeDataType dataType,
      bool isRequired,
      string? optionsCsv,
      string? defaultValue,
      int displayOrder)
  {
    ArgumentNullException.ThrowIfNull(code);
    ArgumentNullException.ThrowIfNull(label);

    var definition = new AttributeDefinition
    {
      Id = attributeDefinitionId,
      Group = group,
      Code = code,
      Label = label,
      DataType = dataType,
      IsRequired = isRequired,
      OptionsCsv = NormalizeOptions(dataType, optionsCsv),
      DisplayOrder = displayOrder,
      IsActive = true
    };

    definition.SetDefaultValue(defaultValue);

    return definition;
  }

  public void Update(
      Code code,
      Name label,
      AttributeDataType dataType,
      bool isRequired,
      string? optionsCsv,
      string? defaultValue,
      int displayOrder)
  {
    ArgumentNullException.ThrowIfNull(code);
    ArgumentNullException.ThrowIfNull(label);

    Code = code;
    Label = label;
    DataType = dataType;
    IsRequired = isRequired;
    OptionsCsv = NormalizeOptions(dataType, optionsCsv);
    DisplayOrder = displayOrder;

    SetDefaultValue(defaultValue);
  }

  public void Activate() => IsActive = true;

  public void Deactivate() => IsActive = false;

  /// Validates a raw data-entry value against this attribute's configured data type.
  public void EnsureValueIsValid(string? value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      if (IsRequired)
        throw new DomainException($"'{Label.Value}' is required.");

      return;
    }

    value = value.Trim();

    switch (DataType)
    {
      case AttributeDataType.Text:
        if (value.Length > MaxTextLength)
          throw new DomainException($"'{Label.Value}' cannot exceed {MaxTextLength} characters.");
        break;

      case AttributeDataType.Number:
        if (!long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
          throw new DomainException($"'{Label.Value}' must be a whole number.");
        break;

      case AttributeDataType.Decimal:
        if (!decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
          throw new DomainException($"'{Label.Value}' must be a decimal number.");
        break;

      case AttributeDataType.Date:
      case AttributeDataType.DateTime:
        if (!DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
          throw new DomainException($"'{Label.Value}' must be a valid date.");
        break;

      case AttributeDataType.Boolean:
        if (!bool.TryParse(value, out _))
          throw new DomainException($"'{Label.Value}' must be either true or false.");
        break;

      case AttributeDataType.Dropdown:
        if (!Options.Contains(value, StringComparer.OrdinalIgnoreCase))
          throw new DomainException($"'{Label.Value}' must be one of: {OptionsCsv}.");
        break;

      case AttributeDataType.MultiSelect:
        var selected = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var item in selected)
        {
          if (!Options.Contains(item, StringComparer.OrdinalIgnoreCase))
            throw new DomainException($"'{Label.Value}' contains an invalid option '{item}'. Allowed: {OptionsCsv}.");
        }
        break;

      case AttributeDataType.File:
        if (!Uri.TryCreate(value, UriKind.Absolute, out _))
          throw new DomainException($"'{Label.Value}' must be a valid file URL.");
        break;
    }
  }

  private void SetDefaultValue(string? defaultValue)
  {
    if (!string.IsNullOrWhiteSpace(defaultValue))
      EnsureValueIsValid(defaultValue);

    DefaultValue = string.IsNullOrWhiteSpace(defaultValue) ? null : defaultValue.Trim();
  }

  private static string? NormalizeOptions(AttributeDataType dataType, string? optionsCsv)
  {
    var requiresOptions = dataType is AttributeDataType.Dropdown or AttributeDataType.MultiSelect;

    if (!requiresOptions)
      return null;

    if (string.IsNullOrWhiteSpace(optionsCsv))
      throw new DomainException($"{dataType} attributes require a list of options.");

    var options = optionsCsv
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToArray();

    if (options.Length == 0)
      throw new DomainException($"{dataType} attributes require a list of options.");

    return string.Join(',', options);
  }
}
