using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Extensions.Helper;

public class AppSettings
{
    public static IConfiguration _configuration { get; set; }

    public static string contentPath { get; set; }

    public AppSettings()
    {
        string path = "appsettings.json";
        _configuration = new ConfigurationBuilder()
            .SetBasePath(contentPath)
            .Add(new JsonConfigurationSource
            {
                Path = path,
                Optional = false,
                ReloadOnChange = true,
            })
            .Build();
    }

    public AppSettings(IConfiguration configuration) => _configuration = configuration;

    public static string app(params string[] sections)
    {
        try
        {
            if (sections.Any())
                return _configuration[string.Join(":", sections)];
        }
        catch
        {

        }
        return "";
    }

    public static List<T> app<T>(params string[] sections)
    {
        List<T> list = new();
        _configuration.Bind(string.Join(":", sections), list);
        return list;
    }
}
