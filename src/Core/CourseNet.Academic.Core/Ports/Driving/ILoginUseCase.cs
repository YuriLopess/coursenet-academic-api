namespace CourseNet.Academic.Core.Ports.Driving;

public interface ILoginUseCase
{
    Task<string> ExecuteAsync(string email, string senha);
}
