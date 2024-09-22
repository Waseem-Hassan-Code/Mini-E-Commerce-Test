using DAL.DTOs;
using DAL.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsData _data;
        public ProductController(IProductsData data)
        {
            _data = data;
        }

        [HttpGet]
        public async Task<ResponseDto> GetProducts(string? searchTerm)
        {
            try
            {
                if (searchTerm?.Length > 30)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Input parameters are too long."
                    };
                }

                var response = await _data.GetProductsAsync(searchTerm);

                if (response == null || !response.Any())
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "No products found matching the criteria."
                    };
                }

                return new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Products retrieved successfully.",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving products.",
                    Result = ex.Message
                };
            }
        }

        [HttpPost]
        public async Task<ResponseDto> AddProducts([FromBody] AddProductDTO model)
        {
            if (model == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Product data must be provided."
                };
            }

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    .ToList();
                string errorMessageString = string.Join(", ", errorMessages);

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid product data.",
                    Result = errorMessageString
                };
            }

            try
            {
                var response = await _data.AddProductAsync(model);
                return new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Product added successfully.",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "An error occurred while adding the product.",
                    Result = ex.Message
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid product ID."
                };
            }

            try
            {
                bool isDeleted = await _data.DeleteProductAsync(id);

                if (isDeleted)
                {
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Product deleted successfully."
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "An error occurred while deleting the product.",
                    Result = ex.Message
                };
            }
        }
        [HttpPut]
        public async Task<ResponseDto> UpdateProduct([FromBody] UpdateProductDTO model)
        {
            if (model == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Product data must be provided."
                };
            }

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                string errorMessageString = string.Join(", ", errorMessages);

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid product data.",
                    Result = errorMessageString
                };
            }

            try
            {
                var response = await _data.UpdateProductAsync(model);

                if (response != null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Product updated successfully.",
                        Result = response
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "An error occurred while updating the product.",
                    Result = ex.Message
                };
            }
        }
    }
}
