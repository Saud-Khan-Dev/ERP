public sealed record FileUrl(string Value)
{
  public static FileUrl Of(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new DomainException("File URL cannot be empty.");

    if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
      throw new DomainException("Invalid file URL.");

    return new FileUrl(uri.ToString());
  }

  public override string ToString() => Value;
}
