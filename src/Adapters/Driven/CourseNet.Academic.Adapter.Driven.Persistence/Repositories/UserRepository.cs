using CourseNet.Academic.Adapter.Driven.Persistence.Context;
using CourseNet.Academic.Core.Domain.Entities;
using CourseNet.Academic.Core.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseNet.Academic.Adapter.Driven.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AcademicDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AcademicDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao consultar usuário por e-mail no banco de dados");
            throw;
        }
    }
}
