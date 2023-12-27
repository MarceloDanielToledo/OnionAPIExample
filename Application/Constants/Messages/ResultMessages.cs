namespace Application.Constants.Messages
{
    public static class ResultMessages
    {
        private static readonly string ValidationErrorMessage = "The validation process has encountered one or more errors.";
        private static readonly string InternalServerErrorMessage = "Internal server error has occurred.";
        private static readonly string NotFoundErrorMessage = "The requested resource could not be found.";
        private static readonly string SuccessfulResponseMessage = "Successful response.";
        public static string ValidationError() => ValidationErrorMessage;
        public static string InternalServerError() => InternalServerErrorMessage;
        public static string NotFoundError() => NotFoundErrorMessage;
        public static string Success() => SuccessfulResponseMessage;
    }
}
