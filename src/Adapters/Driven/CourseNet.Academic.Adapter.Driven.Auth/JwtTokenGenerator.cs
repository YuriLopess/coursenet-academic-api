using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourseNet.Academic.Core.Domain.Entities;
using CourseNet.Academic.Core.Ports.Driven;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CourseNet.Academic.Adapter.Driven.Auth;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _options;
    private readonly ILogger<JwtTokenGenerator> _logger;

    public JwtTokenGenerator(IOptions<JwtOptions> options, ILogger<JwtTokenGenerator> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public string Generate(User user)
    {
        if (string.IsNullOrWhiteSpace(_options.Key))
        {
            _logger.LogCritical("Chave de assinatura do JWT (Jwt:Key) não está configurada");
            throw new InvalidOperationException("Chave de assinatura do JWT não configurada.");
        }

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
