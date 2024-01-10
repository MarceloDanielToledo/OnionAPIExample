using System.Text;

namespace API.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);

            var originalResponseBody = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next.Invoke(context);

                await LogResponse(context, responseBody, originalResponseBody);
            }
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
        {

            var responseContent = new StringBuilder();
            responseContent.AppendLine("=== Response ===");

            responseContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
            responseContent.AppendLine($"path = {context.Request.Path}");
            responseContent.AppendLine($"status = {context.Response.StatusCode}");
            responseContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Response.Headers)
            {
                responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }

            responseContent.AppendLine("-- body");
            responseBody.Position = 0;
            var content = await new StreamReader(responseBody).ReadToEndAsync();
            responseContent.AppendLine($"body = {content}");
            responseBody.Position = 0;
            await responseBody.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;

            _logger.LogInformation(responseContent.ToString());




        }

        private async Task LogRequest(HttpContext context)
        {

            var requestContent = new StringBuilder();

            requestContent.AppendLine("=== Request ===");
            requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
            requestContent.AppendLine($"path = {context.Request.Path}");

            requestContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Request.Headers)
            {
                requestContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }


            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            var content = await requestReader.ReadToEndAsync();
            if (content is not null && content != string.Empty)
            {
                requestContent.AppendLine("-- body");
                requestContent.AppendLine($"body = {content}");
            }


            _logger.LogInformation(requestContent.ToString());
            context.Request.Body.Position = 0;


        }
    }
}
