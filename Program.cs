using DompifyAPI.Application.Interfaces;
using DompifyAPI.Application.UseCases;
using DompifyAPI.Domain.Interfaces;
using DompifyAPI.Infrastructure.Configurations;
using DompifyAPI.Infrastructure.Persistence;
using DompifyAPI.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Host={EnvConfig.DbHost};Database={EnvConfig.DbName};Username={EnvConfig.DbUser};Password={EnvConfig.DbPassword}";

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IOTPRepository, OTPRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<IRoleUseCase, RoleUseCase>();
builder.Services.AddScoped<ICategoryUseCase, CategoryUseCase>();
builder.Services.AddScoped<ICurrencyUseCase, CurrencyUseCase>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();