using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TestApp.Api.Configurations;

/// <summary>
/// Методы расширения для <see cref="IApplicationBuilder"/>
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Сделать миграцию бд для контекста ef
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TContext"></typeparam>
    public static void MigrationDbContext<TContext>(this IApplicationBuilder builder) where TContext : DbContext
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>().Database;
        db.Migrate();

        using var connection = (NpgsqlConnection)db.GetDbConnection();
        connection.Open();
        connection.ReloadTypes();
    }
}