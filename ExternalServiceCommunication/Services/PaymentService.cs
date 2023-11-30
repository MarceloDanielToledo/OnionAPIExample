using Application.Interfaces;
using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Wrappers;
using Polly;
using Polly.Extensions.Http;

namespace ExternalServiceCommunication.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
        public PaymentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(HttpClientNames.GetClientName());
            _retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(2));
        }

        public Task<ExternalResponse<PaymentResponse>> Cancel(string paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalResponse<PaymentResponse>> Create(string terminalId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public async Task<ExternalResponse<PaymentResponse>> GetInfo(string name)
        {

            throw new NotImplementedException();
        }

        public Task<ExternalResponse<PaymentResponse>> Status(string paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
