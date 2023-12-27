using Application.Interfaces;
using EntityGuardian;
using EntityGuardian.Enums;
using EntityGuardian.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .AddInterceptors(serviceProvider.GetRequiredService<EntityGuardianInterceptor>()));
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            services.AddEntityGuardian(
               configuration.GetConnectionString("DefaultConnection"),
                option =>
                {
                    option.StorageType = StorageType.SqlServer;
                    option.DataSynchronizationTimeout = 30;
                    option.ClearDataOnStartup = false;
                    option.RoutePrefix = "entity-guardian";
                    option.EntityGuardianSchemaName = "EntityGuardian";
                });

        }
    }
}
