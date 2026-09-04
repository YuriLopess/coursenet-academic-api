using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourseNet.Academic.Core.Domain.Entities;
using CourseNet.Academic.Core.Ports.Driven;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CourseNet.Academic.Adapter.Driven.Auth;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _options;

    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(User user)
    {
        var claims = new[]
        {
            new Claim("nome", user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(claims: claims, signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
