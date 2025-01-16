public class Appsettings
{
    private IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    public string? Get(string key)
    {
        return config[key];
    }
}