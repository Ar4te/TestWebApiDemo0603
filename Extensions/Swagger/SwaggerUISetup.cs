using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Extensions.Swagger;

public static class SwaggerUISetup
{
    public static void UseSwaggerUISetup(this IApplicationBuilder app)
    {
        app.UseSwaggerUI(c =>
        {
            foreach (var item in ApiVersionConfig.GetApiVersions())
            {
                c.SwaggerEndpoint($"/swagger/{item}/swagger.json", $"{item}");
            }
        });
    }
}
