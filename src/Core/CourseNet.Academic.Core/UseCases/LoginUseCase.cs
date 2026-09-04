using CourseNet.Academic.Core.Domain.Exceptions;
using CourseNet.Academic.Core.Ports.Driven;
using CourseNet.Academic.Core.Ports.Driving;
using Microsoft.Extensions.Logging;

namespace CourseNet.Academic.Core.UseCases;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger<LoginUseCase> _logger;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator,
        ILogger<LoginUseCase> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }

    public async Task<string> ExecuteAsync(string email, string senha)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
        {
            _logger.LogWarning("Tentativa de login para e-mail não cadastrado: {Email}", email);
            throw new InvalidCredentialsException();
        }

        if (!_passwordHasher.Verify(senha, user.Password))
        {
            _logger.LogWarning("Senha incorreta para o e-mail: {Email}", email);
            throw new InvalidCredentialsException();
        }

        return _tokenGenerator.Generate(user);
    }
}
