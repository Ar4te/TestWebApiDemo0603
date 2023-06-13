using Extensions.Helper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Extensions.JWT;

public class TokenHelper
{
    private readonly JwtSecurityTokenHandler _jwtHandler;
    public TokenHelper(JwtSecurityTokenHandler jwtHandler)
    {
        _jwtHandler = jwtHandler;
    }

    public string CreateJwtToken<T>(T user)
    {
        var signingAlogorithm = SecurityAlgorithms.HmacSha256;
        var claimList = this.CreateClaimList(user);
        // Signatrue
        // 取出私钥并以utf8编码字节输出
        var secretByte = Encoding.UTF8.GetBytes(AppSettings.app("JWT", "SecretKey"));
        // 使用非对称算法对私钥进行加密
        var signingkey = new SymmetricSecurityKey(secretByte);
        // 使用HmacSha256来验证加密后的私钥生成数字签名
        var signingCredentials = new SigningCredentials(signingkey, signingAlogorithm);
        // 生成Token
        var token = new JwtSecurityToken(
            issuer: AppSettings.app("JWT", "Issuer"),
            audience: AppSettings.app("JWT", "Audience"),
            claims: claimList,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials);
        var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenStr;
    }

    public T GetToken<T>(string Token)
    {
        Type t = typeof(T);
        object objA = Activator.CreateInstance(t);
        var b = _jwtHandler.ReadJwtToken(Token);
        foreach (var item in b.Claims)
        {
            PropertyInfo _property = t.GetProperty(item.Type);
            if (_property != null && _property.CanRead)
            {
                _property.SetValue(objA, item.Value, null);
            }
        }
        return (T)objA;
    }

    public List<Claim> CreateClaimList<T>(T user)
    {
        var _class = typeof(T);
        List<Claim> claims = new();
        foreach (var item in _class.GetProperties())
        {
            if (item.Name == "Password")
            {
                continue;
            }
            claims.Add(new Claim(item.Name, Convert.ToString(item.GetValue(user))));
        }
        return claims;
    }
}
