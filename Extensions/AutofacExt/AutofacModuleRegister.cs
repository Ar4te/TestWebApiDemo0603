using Autofac;
using Autofac.Extras.DynamicProxy;
using Extensions.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Extensions.AutofacExt;

public class AutofacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // 注册Service层
        var assemblyServices = Assembly.Load("Service");
        builder.RegisterAssemblyTypes(assemblyServices)
            .InstancePerDependency()
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors();

        // 注册Repository层
        var assemblyRepository = Assembly.Load("Repository");
        builder.RegisterAssemblyTypes(assemblyRepository)
            .InstancePerDependency()
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors();
        builder.RegisterType<JwtSecurityTokenHandler>().InstancePerLifetimeScope();
        builder.RegisterType<TokenHelper>().InstancePerLifetimeScope();
    }
}
