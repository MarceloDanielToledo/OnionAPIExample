namespace Application.UseCases.Payments.Jobs
{
    public interface IPaymentJobs
    {
        Task<string> UpdateStatus(int id, string externalId);

    }
}
