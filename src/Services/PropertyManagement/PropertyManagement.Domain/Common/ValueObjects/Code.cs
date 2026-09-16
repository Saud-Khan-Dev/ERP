using System.Text.RegularExpressions;

public sealed record Code
{
  private const int DefaultLength = 50;
  public string Value { get; }

  private readonly static Regex Pattern = new Regex(@"^[A-Z]{3}-\d{3}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

  private Code(string value) => Value = value;

  public static Code Of(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new DomainException("Code is required.");

    if (value.Length > DefaultLength)
      throw new DomainException("Code length is too large.");

    value = value.Trim().ToUpperInvariant();

    if (!Pattern.IsMatch(value))
      throw new DomainException("Code must be in the format ABC-001.");

    return new Code(value);
  }

  public override string ToString() => Value;
}
