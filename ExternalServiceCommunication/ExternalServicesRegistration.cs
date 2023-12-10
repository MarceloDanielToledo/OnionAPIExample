using Application.Interfaces;
using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace ExternalServiceCommunication
{
    public static class ExternalServicesRegistration
    {
        public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ClientValuesConstantsConfig>(configuration.GetSection("ExternalService:ClientValuesConstants"));
            services.Configure<ClientCredentialsConstantsConfig>(configuration.GetSection("ExternalService:Credentials"));

            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(2));

            services.AddHttpClient($"{HttpClientNames.GetClientName()}", client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("ExternalService:URL").Value);
                client.Timeout = TimeSpan.FromSeconds(15);
            }).AddPolicyHandler(retryPolicy);

            services.AddTransient<IPaymentService, PaymentService>();
        }
    }
}