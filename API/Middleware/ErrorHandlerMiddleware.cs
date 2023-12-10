using Application.Constants.Messages;
using Application.Wrappers;
using ExternalServiceCommunication.Constants;
using ExternalServiceCommunication.Exceptions;
using OpenTelemetry.Trace;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message, TraceId = Tracer.CurrentSpan.Context.TraceId.ToString() };
                switch (error)
                {
                    case Application.Exceptions.ApiException e:
                        //custom application error
                        _logger.LogError(e, "ApiException");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = ResultMessages.InternalServerError();
                        responseModel.Succeeded = false;
                        break;
                    case ExternalServiceException e:
                        if (e.Errors.Any())
                        {
                            foreach (var aux in e.Errors)
                            {
                                _logger.LogCritical(e, aux, "ExternalServiceException");
                            }
                        }
                        else
                        {
                            _logger.LogCritical(e, "ExternalServiceException");
                        }
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        responseModel.Message = ExternalResultMessages.ExceptionError();
                        responseModel.Succeeded = false;
                        responseModel.Errors = e.Errors;
                        break;

                    case Application.Exceptions.ValidationException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = ResultMessages.ValidationError();
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = ResultMessages.NotFoundError();
                        responseModel.Succeeded = false;
                        break;
                    default:
                        _logger.LogCritical(error, "UnhandledError");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = ResultMessages.InternalServerError();
                        responseModel.Succeeded = false;
                        break;
                }

                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                };

                var result = JsonSerializer.Serialize(responseModel, options);

                await response.WriteAsync(result);
            }
        }


    }
}
