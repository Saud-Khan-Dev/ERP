using System.Text.RegularExpressions;

public record Code
{
  private const int DefaultLength = 20;
  public string Value { get; }

  private readonly static Regex Pattern = new Regex(@"^CAT-\d{3}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
  private Code(string value) => Value = value;
  public Code Of(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new DomainException("Category code is required.");

    value = value.Trim().ToUpperInvariant();

    if (!Pattern.IsMatch(value))
      throw new DomainException(
          "Category code must be in the format CAT-001.");
          
    return new Code(value);
  }
}