using Application.Interfaces;
using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Utilities;
using ExternalServiceCommunication.Wrappers;
using System.Net;

namespace ExternalServiceCommunication.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ExternalResponse<PaymentResponse>> Cancel(string paymentId)
        {
            // External service communication
            await Task.Delay(1000);
            return new ExternalResponse<PaymentResponse>()
            {
                Success = true,
                CodeState = (int)HttpStatusCode.OK,
                Message = "Success response.",
                Data = new PaymentResponse()
                {
                    Status = "CANCELLED",
                    PaymentId = RandomGenerator.String(),
                    CardBrand = string.Empty,
                    CardType = string.Empty,
                    LastFourDigits = string.Empty,
                }
            };
            throw new NotImplementedException();
        }

        public async Task<ExternalResponse<PaymentResponse>> Create(string terminalId, decimal amount)
        {
            /* I simulate a waiting time and randomly generate a response*/
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
                        Status = "REQUEST",
                        PaymentId = RandomGenerator.String(),
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


            throw new NotImplementedException();
        }

        public Task<ExternalResponse<PaymentResponse>> Status(string paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
