namespace ExternalServiceCommunication.Constants
{
    public static class ExternalResultMessages
    {
        private static readonly string TimeoutMessage = "Timeout.";
        private static readonly string SuccessResponseMessage = "Success response.";
        private static readonly string ErrorResponseMessage = "Response with errors.";
        public static string Timeout() => TimeoutMessage;
        public static string Success() => SuccessResponseMessage;
        public static string Error() => ErrorResponseMessage;
    }
}
