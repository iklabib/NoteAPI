using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string EncryptedPassword { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
}