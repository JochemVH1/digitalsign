namespace digitalsign_api.Controllers.Contracts.V1
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

        public static class Identity
        {
            public const string Token = Base + "/identity/token";
        }
    }
}
