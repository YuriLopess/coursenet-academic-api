namespace CourseNet.Academic.Core.Ports.Driven;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password, string hashedPassword);
}
