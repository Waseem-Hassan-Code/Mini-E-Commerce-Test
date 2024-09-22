using static System.Net.WebRequestMethods;

namespace MVC_WebApp.Enum
{
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public enum ContentType
    {
        Json,
        MultipartFormData,
    }

    public static class SD
    {
        public const string BaseUri = "https://localhost:7053/api/Product";
    }

}
