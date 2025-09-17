using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using TestApp.Core.Settings;
using TestApp.Infrastructure;
using TestApp.Infrastructure.Repositories;

namespace TestApp.Api.Configurations;

public static class ServiceCollectionExtensions
{
    public static void ConfigureSettings<T>(this IServiceCollection services, IConfiguration config, string sectionName) where T : class, new()
    {
        var section = config.GetSection(sectionName);
            if ((object) section.Get<T>() == null)
                throw new InvalidOperationException(sectionName + " section not found in configuration file");
            services.AddOptions<T>().Bind(section).Configure(_ => { });
            T obj = services.BuildServiceProvider().GetRequiredService<IOptions<T>>().Value;
    }
    
    /// <summary>
    /// Configure бд
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DataDbContext>((provider, options) =>
        {
            var dbSettings = provider.GetRequiredService<IOptions<DbSettings>>().Value;
            
            options.UseNpgsql(GetConnectionString(dbSettings),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "public"));
        }, ServiceLifetime.Scoped, ServiceLifetime.Singleton);
        
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
    
    private static string GetConnectionString(DbSettings settings)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = settings.Host,
            Port = int.Parse(settings.Port),
            Database = settings.DbName,
            Username = settings.User,
            Password = settings.Password,
        };

        return connectionStringBuilder.ConnectionString;
    }
}