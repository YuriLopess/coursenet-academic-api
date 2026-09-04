using CourseNet.Academic.Adapter.Driving.Api.Dtos;
using CourseNet.Academic.Core.Domain.Exceptions;
using CourseNet.Academic.Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace CourseNet.Academic.Adapter.Driving.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILoginUseCase loginUseCase, ILogger<AuthController> logger)
    {
        _loginUseCase = loginUseCase;
        _logger = logger;
    }

    /// <summary>Autentica um aluno por e-mail e senha.</summary>
    /// <remarks>Retorna o JWT em texto puro. Enviar como <c>Authorization: Bearer: {token}</c>.</remarks>
    /// <response code="200">Token JWT em texto puro.</response>
    /// <response code="400">E-mail ou senha ausentes.</response>
    /// <response code="401">E-mail ou senha inválidos.</response>
    /// <response code="500">Falha interna (banco de dados ou serviço externo).</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, "text/plain")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, "application/problem+json")]
    public async Task<ContentResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _loginUseCase.ExecuteAsync(request.Email, request.Senha);
            _logger.LogInformation("Login bem-sucedido para {Email}", request.Email);
            return Content(token, "text/plain");
        }
        catch (InvalidCredentialsException ex)
        {
            _logger.LogWarning("Tentativa de login falhou para {Email}", request.Email);
            return new ContentResult
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Content = ex.Message,
                ContentType = "text/plain",
            };
        }
    }
}
