namespace Application.UseCases.Terminals.Constants.Messages
{
    public static class TerminalsMessages
    {
        private static readonly string TerminalNotFound = "The entered terminal does not exist.";

        public static string NotFound() => TerminalNotFound;

    }
}
