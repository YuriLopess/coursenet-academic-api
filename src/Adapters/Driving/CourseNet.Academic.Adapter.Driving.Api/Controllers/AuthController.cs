using CourseNet.Academic.Adapter.Driving.Api.Dtos;
using CourseNet.Academic.Core.Domain.Exceptions;
using CourseNet.Academic.Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace CourseNet.Academic.Adapter.Driving.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    public AuthController(ILoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    public async Task<ContentResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _loginUseCase.ExecuteAsync(request.Email, request.Senha);
            return Content(token, "text/plain");
        }
        catch (InvalidCredentialsException ex)
        {
            return new ContentResult
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Content = ex.Message,
                ContentType = "text/plain",
            };
        }
    }
}
