using Application.Interfaces;
using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Wrappers;

namespace ExternalServiceCommunication.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<ExternalResponse<PaymentResponse>> Cancel(string paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalResponse<PaymentResponse>> Create(string terminalId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalResponse<PaymentResponse>> Status(string paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
