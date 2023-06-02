namespace Identity.Core.Options;

public class JwtOptions
{
	public required string SigningKey { get; set; }
	public required string ValidAudience { get; set; }
	public required string ValidIssuer { get; set; }
	public int ExpiresInMinutes { get; set; }
}