using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Extensions.Swagger;

public static class SwaggerSetup
{
    public static void AddSwaggerSetUp(this IServiceCollection services, string xmlFilePath)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        if (string.IsNullOrEmpty(xmlFilePath) || string.IsNullOrWhiteSpace(xmlFilePath))
            throw new ArgumentNullException(nameof(xmlFilePath));

        services.AddSwaggerGen(options =>
        {
            foreach (var item in ApiVersionConfig.GetApiVersions())
            {
                options.SwaggerDoc(item, new OpenApiInfo
                {
                    Version = item,
                    Title = "API标题",
                    Description = $"API描述，{item}版本",
                    Contact = null,
                    License = null,
                    Extensions = null,
                    TermsOfService = null,
                });
            }

            options.IncludeXmlComments(xmlFilePath);
        });
    }
}
