using Extensions.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Extensions.JWT;

public static class JWTSetup
{
    public static void AddJwtSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            // 获取私钥
            var secretByte = Encoding.UTF8.GetBytes(AppSettings.app("JWT", "SecretKey"));
            options.TokenValidationParameters = new()
            {
                // 验证发布者
                ValidateIssuer = true,
                ValidIssuer = AppSettings.app("JWT", "Issuer"),
                // 验证接收者
                ValidateAudience = true,
                ValidAudience = AppSettings.app("JWT", "Audience"),
                // 验证是否过期
                ValidateLifetime = true,
                // 验证私钥
                IssuerSigningKey = new SymmetricSecurityKey(secretByte)
            };
        });
    }
}
