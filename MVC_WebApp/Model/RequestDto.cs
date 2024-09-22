using MVC_WebApp.Enum;

namespace MVC_WebApp.Model
{
        public class RequestDto
        {
            public ApiType ApiType { get; set; } = ApiType.GET;
            public string Url { get; set; }
            public object Data { get; set; }
            public MVC_WebApp.Enum.ContentType ContentType { get; set; } = MVC_WebApp.Enum.ContentType.Json;
        }
}
