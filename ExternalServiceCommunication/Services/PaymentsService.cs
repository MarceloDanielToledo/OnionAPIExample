using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Constants;
using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Services.Interfaces;
using ExternalServiceCommunication.Utilities;
using ExternalServiceCommunication.Wrappers;
using System.Net;

namespace ExternalServiceCommunication.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ExternalResponse<PaymentResponse>> Cancel(string paymentId)
        {
            /* I simulate a waiting time and generate a response*/
            await Task.Delay(1000);
            return new ExternalResponse<PaymentResponse>()
            {
                Success = true,
                CodeState = (int)HttpStatusCode.OK,
                Message = "Success response.",
                Data = new PaymentResponse()
                {
                    Status = StatusConstants.Cancelled,
                    Id = RandomGenerator.String(),
                    CardBrand = string.Empty,
                    CardType = string.Empty,
                    LastFourDigits = string.Empty,
                }
            };
        }

        public async Task<ExternalResponse<PaymentResponse>> Create(string terminalId, decimal amount)
        {
            /* I simulate a waiting time and randomly generate a response*/
            using var response = await _httpClientFactory.CreateClient(HttpClientNames.GetClientName()).GetAsync("");
            await Task.Delay(1500);
            Random random = new Random();
            var randomNumber = random.Next(0, 100);
            if (randomNumber % 2 == 0)
            {
                return new ExternalResponse<PaymentResponse>()
                {
                    Success = true,
                    CodeState = (int)HttpStatusCode.OK,
                    Message = "Success response.",
                    Data = new PaymentResponse()
                    {
                        Status = StatusConstants.Request,
                        Id = RandomGenerator.String(),
                        CardBrand = string.Empty,
                        CardType = string.Empty,
                        LastFourDigits = string.Empty,
                    }
                };

            }
            else
            {
                return new ExternalResponse<PaymentResponse>()
                {
                    Success = false,
                    CodeState = (int)HttpStatusCode.BadRequest,
                    Message = "Can´t generate a payment"
                };

            }
        }

        public async Task<ExternalResponse<PaymentResponse>> Status(string paymentId)
        {
            using var response = await _httpClientFactory.CreateClient(HttpClientNames.GetClientName()).GetAsync("");
            return new ExternalResponse<PaymentResponse>()
            {
                Success = true,
                CodeState = (int)HttpStatusCode.OK,
                Message = "Success response.",
                Data = new PaymentResponse()
                {
                    Status = StatusConstants.Confirmed,
                    Id = RandomGenerator.String(),
                    CardBrand = "1234",
                    CardType = "CREDIT",
                    LastFourDigits = "1234",
                }
            };
        }
    }
}
