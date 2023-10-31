using OpenTelemetry.Trace;

namespace Application.Wrappers
{
    public class Response<T>
    {

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public Response() { }

        public Response(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            TraceId = Tracer.CurrentSpan.Context.TraceId.ToString();

        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            TraceId = Tracer.CurrentSpan.Context.TraceId.ToString();

        }
        public Response(bool succeeded, string message, List<string> errors, T data)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors;
            TraceId = Tracer.CurrentSpan.Context.TraceId.ToString();
            Data = data;
        }
        public Response(bool succeeded, List<string> errors, string message)
        {
            Succeeded = succeeded;
            Message = message;
            TraceId = Tracer.CurrentSpan.Context.TraceId.ToString();
            Errors = errors;
        }

        public static Response<T> NotFoundRecord(string message)
        {
            return new Response<T>(message);
        }
        public static Response<T> BadRequest(string message)
        {
            return new Response<T>(message);
        }
        public static Response<T> SuccessResponse(T data, string message)
        {
            return new Response<T>(data, message);
        }
        public static Response<T> SuccessResponseJob(T data, string message, string traceId)
        {
            return new Response<T> { Data = data, Message = message, TraceId = traceId, Succeeded = true };

        }
        public static Response<T> BadResponseJob(T data, string message, string traceId)
        {
            return new Response<T> { Data = data, Message = message, TraceId = traceId, Succeeded = false };

        }

    }
}
