namespace ExternalServiceCommunication.Routes.Name
{
    public static class NameRoutes
    {
        private readonly static string GetName = "?name[]=";


        public static string GetByName(string name) => GetName + $"{name}";
    }
}
