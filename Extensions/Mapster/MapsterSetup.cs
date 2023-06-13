using Microsoft.Extensions.DependencyInjection;
using Mapster;
using MapsterMapper;

namespace Extensions.Mapster;

public static class MapsterSetup
{
    public static void AddMapsterSetup(this IServiceCollection services,TypeAdapterConfig adapterCfg)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        services.AddSingleton(adapterCfg);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}