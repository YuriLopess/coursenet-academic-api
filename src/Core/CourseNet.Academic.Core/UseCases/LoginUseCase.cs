using CourseNet.Academic.Core.Domain.Exceptions;
using CourseNet.Academic.Core.Ports.Driven;
using CourseNet.Academic.Core.Ports.Driving;

namespace CourseNet.Academic.Core.UseCases;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> ExecuteAsync(string email, string senha)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null || !_passwordHasher.Verify(senha, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        return _tokenGenerator.Generate(user);
    }
}
