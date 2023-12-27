using Application.Behaviours;
using Application.UseCases.Payments.Jobs;
using FluentValidation;
using MediatR;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IPaymentJobs, PaymentJobs>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
