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

    /// <summary>
    /// Autentica um aluno a partir de e-mail e senha.
    /// </summary>
    /// <remarks>
    /// Em caso de sucesso, o corpo da resposta é o token JWT em <b>texto puro</b> — não é um
    /// objeto JSON, é a string do token diretamente, sem aspas nem envelope.
    /// Esse token deve ser enviado nas próximas requisições autenticadas no header
    /// <c>Authorization: Bearer: {token}</c>.
    /// </remarks>
    /// <param name="request">E-mail e senha do aluno.</param>
    /// <response code="200">
    /// Login realizado com sucesso. Corpo da resposta: token JWT em texto puro (content-type <c>text/plain</c>).
    /// </response>
    /// <response code="400">
    /// Requisição malformada — e-mail ou senha não foram enviados no corpo da requisição.
    /// Validações de formato (e-mail válido, senha mínima, etc.) ainda serão adicionadas.
    /// </response>
    /// <response code="401">E-mail ou senha não conferem com nenhum aluno cadastrado.</response>
    /// <response code="500">
    /// Falha interna ao processar o login — por exemplo, banco de dados indisponível
    /// ou outro serviço externo fora do ar. O corpo da resposta não expõe detalhes internos.
    /// </response>
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
