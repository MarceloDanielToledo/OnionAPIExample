using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public List<string> Errors { get; private set; } = new();
        public ApiException(int statuscode, List<string> errors) : base()
        {
            StatusCode = statuscode;
            Errors = errors;
        }
        public ApiException(int statuscode, string error) : base()
        {
            StatusCode = statuscode;
            Errors.Add(error);
        }
        public ApiException(string error) : base()
        {
            Errors.Add(error);
        }
        public ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
