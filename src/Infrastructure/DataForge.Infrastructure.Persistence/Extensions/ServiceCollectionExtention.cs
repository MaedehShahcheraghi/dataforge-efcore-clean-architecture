using System.Data;
using System.Reflection;
using DataForge.Infrastructure.Persistence.Contexts;
using DataForge.Infrastructure.Persistence.DataSeed;
using DataForge.Infrastructure.Persistence.Seeding.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataForge.Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlServer"),
                    sqlServerOptions =>
                    {
                        var migrationsAssemblyName =
                            typeof(ApplicationDbContext)
                                .Assembly
                                .GetName()
                                .Name!;

                        sqlServerOptions.MigrationsAssembly(
                            migrationsAssemblyName);
                    });
            });

            RegisterSeeders(
                services,
                typeof(ServiceCollectionExtension).Assembly);
            return services;
        }

        private static void RegisterSeeders(
            IServiceCollection services,
            Assembly assembly)
        {
            var seederType = typeof(IDataSeeder);

            var implementations = assembly
                .GetTypes()
                .Where(type =>
                    type is
                    {
                        IsClass: true,
                        IsAbstract: false
                    } &&
                    seederType.IsAssignableFrom(type));

            foreach (var implementation in implementations)
            {
                services.AddScoped(
                    seederType,
                    implementation);
            }
        }
    }

    

}
