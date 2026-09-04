using CourseNet.Academic.Adapter.Driven.Persistence.Context;
using CourseNet.Academic.Core.Domain.Entities;
using CourseNet.Academic.Core.Ports.Driven;
using Microsoft.EntityFrameworkCore;

namespace CourseNet.Academic.Adapter.Driven.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AcademicDbContext _context;

    public UserRepository(AcademicDbContext context)
    {
        _context = context;
    }

    public async Task<User?> FindByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
}
