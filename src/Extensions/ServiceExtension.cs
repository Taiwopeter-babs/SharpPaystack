using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SharpPayStack.Models;
using SharpPayStack.Validators;
using SharpPayStack.Interfaces;
using SharpPayStack.Services;
using SharpPayStack.Repository;
using SharpPayStack.Data;
using SharpPayStack.Utilities;
using Refit;


namespace SharpPayStack.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// Register database context
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string? databaseConnectionString;

        if (environment == "Development")
        {
            databaseConnectionString = configuration.GetConnectionString("PostgresqlDatabase");
        }
        else
        /// Get database string from secrets vault
        {
            databaseConnectionString = configuration.GetConnectionString("PostgresqlDatabaseStaging");
        }

        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(
                databaseConnectionString,
                op => op.MigrationsHistoryTable("SharpPaystackMigrationsHistory")));
    }

    /// <summary>
	/// Configure cors for application
	/// </summary>
	/// <param name="services"></param>
	public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders()
        .AddRoles<IdentityRole>();
    }

    /// <summary>
    /// Configure a scoped repositories and services manager dependency injection
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureManagers(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    /// <summary>
    /// Configure Payment Services
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigurePaymentServices(this IServiceCollection services)
    {
        services.AddScoped<IPaystackService, PaystackService>();
        services.AddScoped<IAccountService, AccountService>();
    }

    // <summary>
    /// Register validators
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterUserDto>, UserValidator>();
    }

    // <summary>
    /// Register validators
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PaystackOptions>(
            configuration.GetSection(key: nameof(PaystackOptions))
        );
    }

    public static void ConfigureApiClients(this IServiceCollection services)
    {
        services.AddRefitClient<IPaystackApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.paystack.co"));
    }
}