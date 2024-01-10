namespace Application.UseCases.Refunds.Constants.Messages
{
    public static class RefundsMessages
    {
        private static readonly string CreatedConstant = "Refund request created successfully.";
        private static readonly string CanceledErrorByStatusConstant = "The refund does not have a pending state for cancellation.";
        private static readonly string AlreadyExistConstant = "";
        private static readonly string RequestCanceledConstant = "Refund request successfully canceled.";
        private static readonly string CanceledErrorConstant = "Error canceling refund.";
        public static string Created() => CreatedConstant;
        public static string AlreadyExist() => AlreadyExistConstant;
        public static string CanceledErrorIncorrectStatus() => CanceledErrorByStatusConstant;
        public static string Canceled() => RequestCanceledConstant;
        public static string CanceledError() => CanceledErrorConstant;
    }
}
