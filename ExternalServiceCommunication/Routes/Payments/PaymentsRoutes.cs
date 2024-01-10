namespace ExternalServiceCommunication.Routes.Name
{
    public static class PaymentsRoutes
    {
        private readonly static string GetName = "?name[]=";


        public static string GetByName(string name) => GetName + $"{name}";
    }
}
