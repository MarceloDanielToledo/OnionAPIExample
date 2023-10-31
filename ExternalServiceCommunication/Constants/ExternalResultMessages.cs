namespace ExternalServiceCommunication.Constants
{
    public static class ExternalResultMessages
    {
        private static readonly string TimeoutMessage = "Timeout.";
        private static readonly string SuccessResponseMessage = "Success.";
        public static string Timeout() => TimeoutMessage;
        public static string Success() => SuccessResponseMessage;
    }
}
