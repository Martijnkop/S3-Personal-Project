namespace barboek.Auth;

public class Account
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry {  get; set; }
}