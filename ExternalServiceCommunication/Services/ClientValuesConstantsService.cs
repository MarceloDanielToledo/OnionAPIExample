using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace ExternalServiceCommunication.Services
{
    internal class ClientValuesConstantsService : IClientValuesConstantsService
    {
        private readonly ClientValuesConstantsConfig _config;

        internal ClientValuesConstantsService(IOptions<ClientValuesConstantsConfig> config)
        {
            _config = config.Value;
        }
        public string ClientConfigValueExample() => _config.ClientConfigValueExample;

        public string ClientConfigValueExample2() => _config.ClientConfigValueExample2;
    }
}
