using ExternalServiceCommunication.Models.Name;
using ExternalServiceCommunication.Wrappers;

namespace Application.Interfaces
{
    public interface INameService
    {
        Task<ExternalResponse<GetName>> GetInfo(string name);
    }
}
