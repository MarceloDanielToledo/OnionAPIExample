﻿using Application.Interfaces;
using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalServiceCommunication
{
    public static class ExternalServicesRegistration
    {
        public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ClientValuesConstantsConfig>(configuration.GetSection("ExternalService:ClientValuesConstants"));
            services.Configure<ClientCredentialsConstantsConfig>(configuration.GetSection("ExternalService:Credentials"));



            services.AddTransient<IPaymentService, PaymentService>();
            services.AddHttpClient($"{HttpClientNames.GetClientName()}", client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("ExternalService:URL").Value);
                client.Timeout = TimeSpan.FromSeconds(15);
            });
        }
    }
}