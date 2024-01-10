using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.HttpHandlers;
using ExternalServiceCommunication.Services;
using ExternalServiceCommunication.Services.Interfaces;
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
            services.AddTransient<HttpLoggerHandler>();
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(2));
            var externalUrl = configuration.GetSection("ExternalService:URL").Value ?? string.Empty;
            services.AddHttpClient($"{HttpClientNames.GetClientName()}", client =>
            {
                client.BaseAddress = new Uri(externalUrl);
                client.Timeout = TimeSpan.FromSeconds(15);
            }).AddHttpMessageHandler<HttpLoggerHandler>().AddPolicyHandler(retryPolicy);

            services.AddTransient<IPaymentsService, PaymentsService>();
            services.AddTransient<IRefundsService, RefundsService>();
        }
    }
}