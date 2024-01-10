using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Constants;
using ExternalServiceCommunication.Models.Refunds;
using ExternalServiceCommunication.Services.Interfaces;
using ExternalServiceCommunication.Utilities;
using ExternalServiceCommunication.Wrappers;
using System.Net;

namespace ExternalServiceCommunication.Services
{
    public class RefundsService : IRefundsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RefundsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ExternalResponse<RefundResponse>> Create(string paymentId, string reason)
        {
            using var response = await _httpClientFactory.CreateClient(HttpClientNames.GetClientName()).GetAsync("");
            await Task.Delay(1500);
            return new ExternalResponse<RefundResponse>()
            {
                Success = true,
                CodeState = (int)HttpStatusCode.OK,
                Message = "Success response.",
                Data = new RefundResponse()
                {
                    Id = RandomGenerator.String(),
                    Status = StatusConstants.Request
                }
            };
        }
        public Task<ExternalResponse<RefundResponse>> Cancel(string refundId)
        {
            throw new NotImplementedException();
        }
        public Task<ExternalResponse<RefundResponse>> Status(string refundId)
        {
            throw new NotImplementedException();
        }
    }
}
