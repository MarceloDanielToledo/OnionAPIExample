using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var aasm = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(aasm);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(aasm));
        }
    }
}
