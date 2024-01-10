using ExternalServiceCommunication.Models.Refunds;
using ExternalServiceCommunication.Wrappers;

namespace ExternalServiceCommunication.Services.Interfaces
{
    public interface IRefundsService
    {
        Task<ExternalResponse<RefundResponse>> Create(string paymentId, string reason);
        Task<ExternalResponse<RefundResponse>> Status(string refundId);
        Task<ExternalResponse<RefundResponse>> Cancel(string refundId);

    }
}
