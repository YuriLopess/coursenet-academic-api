using CourseNet.Academic.Core.Domain.Entities;

namespace CourseNet.Academic.Core.Ports.Driven;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email);
}
