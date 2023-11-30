using ExternalServiceCommunication.Configs;
using ExternalServiceCommunication.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace ExternalServiceCommunication.Services
{
    internal class ClientCredentialsConstantsService : IClientCredentialsConstantsService
    {
        private readonly ClientCredentialsConstantsConfig _config;

        internal ClientCredentialsConstantsService(IOptions<ClientCredentialsConstantsConfig> config)
        {
            _config = config.Value;
        }
        public string BasicCredential() => _config.BasicCredential;
    }
}
