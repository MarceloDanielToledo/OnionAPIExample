using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Wrappers;

namespace ExternalServiceCommunication.Services.Interfaces
{
    public interface IPaymentsService
    {
        Task<ExternalResponse<PaymentResponse>> Create(string terminalId, decimal amount);
        Task<ExternalResponse<PaymentResponse>> Status(string paymentId);
        Task<ExternalResponse<PaymentResponse>> Cancel(string paymentId);
    }
}
