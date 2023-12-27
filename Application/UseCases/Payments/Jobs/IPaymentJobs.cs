namespace Application.UseCases.Payments.Jobs
{
    public interface IPaymentJobs
    {
        Task<string> UpdatePaymentStatus(int id, string externalId);

    }
}
