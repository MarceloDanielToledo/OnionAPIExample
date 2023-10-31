using System.Runtime.Serialization;

namespace ExternalServiceCommunication.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public ExternalServiceException(int statuscode, List<string> errors) : base()
        {
            StatusCode = statuscode;
            Errors = errors;
        }
        public ExternalServiceException(int statuscode, string error) : base()
        {
            StatusCode = statuscode;
            Errors.Add(error);
        }
        public ExternalServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
