namespace Application.UseCases.Payments.Constants.Messages
{
    public static class PaymentsMessages
    {
        private static readonly string PaymentRequestCreatedConstant = "Payment request created successfully.";
        private static readonly string PaymentRequestCanceledConstant = "Payment request successfully canceled.";
        private static readonly string PaymentNotFoundConstant = "Payment not found.";
        private static readonly string PaymentCanceledErrorConstant = "Error canceling payment.";
        private static readonly string PaymentCanceledErrorByStatusConstant = "The payment does not have a pending state for cancellation.";
        private static readonly string PaymentRefundErrorByStatusConstant = "The payment does a incorrect state for refund.";
        private static readonly string PaymentUpdatedInDbConstant = "The payment was updated in the database.";
        private static readonly string UpdatedInJobConstant = "Payment status successfully updated.";
        public static string RequestCreated() => PaymentRequestCreatedConstant;
        public static string Canceled() => PaymentRequestCanceledConstant;
        public static string NotFound() => PaymentNotFoundConstant;
        public static string CanceledError() => PaymentCanceledErrorConstant;
        public static string CanceledErrorIncorrectStatus() => PaymentCanceledErrorByStatusConstant;
        public static string RefundErrorIncorrectStatus() => PaymentRefundErrorByStatusConstant;
        public static string UpdatedInDB() => PaymentUpdatedInDbConstant;
        public static string UpdatedInJob() => UpdatedInJobConstant;



    }
}
