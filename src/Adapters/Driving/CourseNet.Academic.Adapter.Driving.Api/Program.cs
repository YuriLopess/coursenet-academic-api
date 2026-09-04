using System.Reflection;
using CourseNet.Academic.Adapter.Driven.Auth;
using CourseNet.Academic.Adapter.Driven.Persistence.Context;
using CourseNet.Academic.Adapter.Driven.Persistence.Repositories;
using CourseNet.Academic.Adapter.Driving.Api.Middleware;
using CourseNet.Academic.Core.Ports.Driven;
using CourseNet.Academic.Core.Ports.Driving;
using CourseNet.Academic.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CourseNet Academic API",
        Version = "v1",
        Description = "API responsável pela autenticação e pelos dados acadêmicos dos alunos da CourseNet.",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services.AddDbContext<AcademicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Academic")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
