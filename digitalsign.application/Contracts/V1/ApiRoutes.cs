namespace digitalsign_api.Contracts.V1
{
    public static class ApiRoutes
    {

        private const string ROUTE = "api";
        private const string VERSION = "v1";

        private const string BASE = ROUTE + "/" + VERSION;

        public static class Message
        {
            public const string Get = BASE + "/message";
        }

        public static class Idenity
        {
            public const string Login = BASE + "/login";
            public const string Register = BASE + "/register";
            public const string Refresh = BASE + "/refresh";
        }
    }
}
