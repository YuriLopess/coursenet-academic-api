using CourseNet.Academic.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseNet.Academic.Adapter.Driven.Persistence.Context;

public class AcademicDbContext : DbContext
{
    public AcademicDbContext(DbContextOptions<AcademicDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademicDbContext).Assembly);
    }
}
