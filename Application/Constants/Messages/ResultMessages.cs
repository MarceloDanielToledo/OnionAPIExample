namespace Application.Constants.Messages
{
    public static class ResultMessages
    {
        private static readonly string ValidationErrorMessage = "The validation process has encountered one or more errors.";
        private static readonly string InternalServerErrorMessage = "The validation process has encountered one or more errors.";
        private static readonly string NotFoundErrorMessage = "The requested resource could not be found.";
        public static string ValidationError() => ValidationErrorMessage;
        public static string InternalServerError() => InternalServerErrorMessage;
        public static string NotFoundError() => NotFoundErrorMessage;
    }
}
