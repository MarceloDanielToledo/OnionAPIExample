namespace Application.Constants.Messages
{
    public static class TerminalMessages
    {
        private static readonly string TerminalNotFound = "The entered terminal does not exist.";

        public static string NotFound() => TerminalNotFound;

    }
}
