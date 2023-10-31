using Application.Interfaces;
using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Wrappers;
using Polly.Extensions.Http;
using Polly;
using System.Net.Http;
using System.Text;

namespace ExternalServiceCommunication.Services
{
    public class NameService : INameService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
        public NameService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(HttpClientNames.GetClientName());
            _retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(2));
        }
        public async Task<ExternalResponse<GetName>> GetInfo(string name)
        {

            throw new NotImplementedException();
        }
    }
}
