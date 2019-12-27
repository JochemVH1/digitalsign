namespace digitalsign_api.Contracts.V1
{
    public static class ApiRoutes
    {

        private const string Route = "api";
        private const string Version = "v1";

        private const string Base = Route + "/" + Version;
        
        public static class Message
        {
            public const string Get = Base + "/message";
            public const string GetById = Base + "/message/{id}";
            public const string Post = Base + "/message";
        }

        public static class Idenity
        {
            public const string Login = Base + "/login";
            public const string Register = Base + "/register";
            public const string Refresh = Base + "/refresh";
        }
    }
}
