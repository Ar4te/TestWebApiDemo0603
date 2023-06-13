using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Extensions.Swagger;

public enum ApiVersionInfo
{
    [ApiGroupInfo("V1")]
    V1,
    [ApiGroupInfo("V2")]
    V2,
    [ApiGroupInfo("V3")]
    V3,
    [ApiGroupInfo("V4")]
    V4,
    [ApiGroupInfo("V5")]
    V5,
    [ApiGroupInfo("V6")]
    V6
}

public class ApiVersionConfig
{
    public static string[] GetApiVersions() => typeof(ApiVersionInfo).GetEnumNames();
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ApiRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
{
    public string GroupName { get; set; }
    public ApiRouteAttribute(string actionName = "[action]") : base($"/api/{actionName}/[controller]/")
    {
    }
    public ApiRouteAttribute(ApiVersionInfo version, bool isVersionFilter = true, string api = "api", string controllerName = "[controller]", string actionName = "[action]") : base(isVersionFilter ? $"/{api}/{controllerName}/{version}/{actionName}" : $"/{api}/{controllerName}/{actionName}")
    {
        GroupName = version.ToString();
    }
}

[AttributeUsage(AttributeTargets.All)]
public class ApiGroupInfoAttribute : Attribute
{
    public ApiGroupInfoAttribute(string value)
    {
        Title = value;
        Version = value;
        Description = value;
    }
    public ApiGroupInfoAttribute(string title, string version, string description)
    {
        Title = title;
        Version = version;
        Description = description;
    }

    public string Title { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
}