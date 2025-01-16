using System.Text.Json.Serialization;

public class RegisterDTO
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

public class LoginDTO
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
public class NewChecklistDTO 
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class NewChecklistItemDTO 
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class UpdateChecklistItemDTO 
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

