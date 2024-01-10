using Microsoft.Extensions.Logging;
using System.Text;

namespace ExternalServiceCommunication.HttpHandlers
{
    public class HttpLoggerHandler : DelegatingHandler
    {
        private readonly ILogger<HttpLoggerHandler> _logger;

        public HttpLoggerHandler(ILogger<HttpLoggerHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await LogRequest(request);
            var response = await base.SendAsync(request, cancellationToken);
            await LogResponse(request, response);
            return response;
        }



        private async Task LogRequest(HttpRequestMessage request)
        {
            var requestContent = new StringBuilder();

            requestContent.AppendLine("=== External Request ===");
            requestContent.AppendLine($"method = {request.Method.Method.ToUpper()}");
            requestContent.AppendLine($"uri = {request.RequestUri}");

            requestContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in request.Headers)
            {
                requestContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }
            if (request.Content is not null)
            {
                requestContent.AppendLine("-- body");
                var content = await request.Content.ReadAsStringAsync();
                requestContent.AppendLine($"body = {content}");
            }
            _logger.LogInformation(requestContent.ToString());
        }

        private async Task LogResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
                var responseContent = new StringBuilder();
                responseContent.AppendLine("=== External Response ===");
                responseContent.AppendLine($"uri = {request.RequestUri.OriginalString}");
                responseContent.AppendLine($"method = {request.Method.Method.ToUpper()}");
                responseContent.AppendLine($"status = {response.StatusCode}");
                responseContent.AppendLine("-- headers");
                foreach (var (headerKey, headerValue) in request.Headers)
                {
                    responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
                }

                responseContent.AppendLine("-- body");
                var content = await response.Content.ReadAsStringAsync();
                responseContent.AppendLine($"body = {content}");
                _logger.LogInformation(responseContent.ToString());

        }
    }
}
