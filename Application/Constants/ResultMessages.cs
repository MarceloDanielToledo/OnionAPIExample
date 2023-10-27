namespace Application.Constants
{
    public static class ResultMessages
    {
        private static readonly string ValidationErrorMessage = "The validation process has encountered one or more errors.";


        public static string ValidationError() => ValidationErrorMessage;
    }
}
