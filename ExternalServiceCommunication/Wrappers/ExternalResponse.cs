using ExternalServiceCommunication.Constants;
using System.Net;

namespace ExternalServiceCommunication.Wrappers
{
    public class ExternalResponse<T> where T : class
    {
        public bool Success { get; set; }
        public int CodeState { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ExternalResponse(bool success, T data, HttpResponseMessage httpData)
        {
            Success = success;
            CodeState = (int)httpData.StatusCode;
            Message = ExternalResultMessages.Success();
            Data = data;
        }
        public ExternalResponse(bool success, string message, int codeState)
        {
            Success = success;
            CodeState = codeState;
            Message = message;

        }
        public static ExternalResponse<T> SuccessResponse(T data, HttpResponseMessage httpData)
        {
            return new ExternalResponse<T>(true, data, httpData);
        }

        public static ExternalResponse<T> ErrorResponse(HttpResponseMessage httpData, string message)
        {
            return new ExternalResponse<T>(false, message, (int)httpData.StatusCode);
        }
        public static ExternalResponse<T> ExceptionError(Exception ex)
        {
            return new ExternalResponse<T>(false, ex.Message, (int)HttpStatusCode.InternalServerError);
        }

    }
}
