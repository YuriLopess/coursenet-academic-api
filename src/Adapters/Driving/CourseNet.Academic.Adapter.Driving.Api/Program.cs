using CourseNet.Academic.Adapter.Driven.Auth;
using CourseNet.Academic.Adapter.Driven.Persistence.Context;
using CourseNet.Academic.Adapter.Driven.Persistence.Repositories;
using CourseNet.Academic.Core.Ports.Driven;
using CourseNet.Academic.Core.Ports.Driving;
using CourseNet.Academic.Core.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services.AddDbContext<AcademicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Academic")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
