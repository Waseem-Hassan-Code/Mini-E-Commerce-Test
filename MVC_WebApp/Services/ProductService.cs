using MVC_WebApp.Enum;
using MVC_WebApp.Model;
using MVC_WebApp.Services.IServices;

namespace MVC_WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AddProductAsync(AddProductDto dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = SD.BaseUri
            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.BaseUri+ $"/{id}"
            });
        }

        public async Task<ResponseDto?> GetProductsAsync(string? searchTerm)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.BaseUri + $"?searchTerm={searchTerm}"
            });
        }
        
        public async Task<ResponseDto?> UpdateProductAsync(UpdateProductDto dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = dto,
                Url = SD.BaseUri
            });
        }
    }
}
