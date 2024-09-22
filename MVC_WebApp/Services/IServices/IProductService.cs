using MVC_WebApp.Model;

namespace MVC_WebApp.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductsAsync(string? searchTerm);
        Task<ResponseDto?> AddProductAsync(AddProductDto dto);
        Task<ResponseDto?> DeleteProductAsync(int id);
        Task<ResponseDto?> UpdateProductAsync(UpdateProductDto dto);
    }
}
