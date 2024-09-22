

using MVC_WebApp.Model;

namespace MVC_WebApp.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
