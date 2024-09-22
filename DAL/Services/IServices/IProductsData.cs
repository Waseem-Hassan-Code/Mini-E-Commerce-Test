using DAL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services.IServices
{
    public interface IProductsData
    {
        Task<Product> AddProductAsync(AddProductDTO product);
        Task<UpdateProductDTO> UpdateProductAsync(UpdateProductDTO product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync(string? searchTerm);
    }
}
