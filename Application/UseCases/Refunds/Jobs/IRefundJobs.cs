namespace Application.UseCases.Refunds.Jobs
{
    public interface IRefundJobs
    {
        Task<string> UpdateStatus(int id, string externalId);
    }
}
