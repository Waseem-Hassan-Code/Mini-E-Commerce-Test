using DAL.ApplicationDb;
using DAL.DTOs;
using DAL.Models;
using DAL.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ProductsData : IProductsData
    {
        private readonly AppDbContext _context;
        public ProductsData(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<Product> AddProductAsync(AddProductDTO product)
        {
            try
            {
                var prod = new Product
                {
                    Name = product.Name,
                    Category = product.Category,
                    Price = product.Price,
                    Description = product.Description,
                    CreatedDate = DateTime.UtcNow, 
                };

                await _context.AddAsync(prod);
                await _context.SaveChangesAsync();

                return prod;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Product_.FindAsync(id);

                if (product == null)
                {
                    return false;
                }

                _context.Product_.Remove(product);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string? searchTerm)
        {
            try
            {
                var query = _context.Product_.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var lowerCaseSearchTerm = searchTerm.ToLower();
                    query = query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm) ||
                                              p.Category.ToLower().Contains(lowerCaseSearchTerm));
                }

                return await query.OrderBy(p => p.CreatedDate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<UpdateProductDTO> UpdateProductAsync(UpdateProductDTO product)
        {
            try
            {
                var prod = await _context.Product_.FindAsync(product.Id);
                if (prod == null)
                {
                    throw new KeyNotFoundException("Product not found.");
                }

                prod.Name = product.Name;
                prod.Price = product.Price;
                prod.Description = product.Description;
                prod.Category = product.Category;
                prod.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return product;
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new Exception("Product not found.", knfEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }

    }
}
