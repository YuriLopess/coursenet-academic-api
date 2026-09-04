namespace CourseNet.Academic.Core.Domain.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Usuário ou senha inválidos")
    {
    }
}
