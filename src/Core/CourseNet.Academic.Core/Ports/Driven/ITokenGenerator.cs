using CourseNet.Academic.Core.Domain.Entities;

namespace CourseNet.Academic.Core.Ports.Driven;

public interface ITokenGenerator
{
    string Generate(User user);
}
