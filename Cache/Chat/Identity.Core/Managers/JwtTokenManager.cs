using System.IdentityModel.Tokens.Jwt;
using Identity.Core.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Identity.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Core.Managers;

public class JwtTokenManager
{
    private readonly JwtOptions _jwtOption;

    public JwtTokenManager(IOptions<JwtOptions> jwtOption)
    {
        _jwtOption = jwtOption.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var signingKey = System.Text.Encoding.UTF32.GetBytes(_jwtOption.SigningKey);
        var security = new JwtSecurityToken(
            issuer: _jwtOption.ValidIssuer,
            audience: _jwtOption.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtOption.ExpiresInMinutes),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(security);

        return token;
    }
}