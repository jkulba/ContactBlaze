using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Extensions;

/// <summary>
/// Service collection extensions for the Infrastructure layer.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext with SQLite
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=contacts.db";

        services.AddDbContext<ContactBlazeDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
