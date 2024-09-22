using DAL.DTOs;
using DAL.Models;
using DAL.Services.IServices;
using Moq;
using MVC_WebApp.Model;
using WebAPIs.Controllers;
using Xunit;

namespace WebAPIsUnitTests
{
    public class AddProductAPITests
    {
        private readonly Mock<IProductsData> _mockData;
        private readonly ProductController _controller;

        public AddProductAPITests()
        {
            _mockData = new Mock<IProductsData>();
            _controller = new ProductController(_mockData.Object);
        }

        [Fact]
        public async Task AddProducts_ValidModel_ReturnsSuccessResponse()
        {
            var model = new AddProductDTO
            {
                Name = "New Product",
                Category = "New Category",
                Price = 100,
                Description = "New Description"
            };

            var addedProduct = new Product
            {
                Id = 1,
                Name = model.Name,
                Category = model.Category,
                Price = model.Price,
                Description = model.Description,
                CreatedDate = DateTime.UtcNow
            };

            _mockData.Setup(service => service.AddProductAsync(model))
                .ReturnsAsync(addedProduct);

            var result = await _controller.AddProducts(model);

            Assert.True(result.IsSuccess);
            Assert.Equal("Product added successfully.", result.Message);
            Assert.Equal(addedProduct, result.Result);
        }

        [Fact]
        public async Task AddProducts_NullModel_ReturnsErrorResponse()
        {
            AddProductDTO model = null;

            var result = await _controller.AddProducts(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("Product data must be provided.", result.Message);
        }

        [Fact]
        public async Task AddProducts_InvalidModelState_ReturnsErrorResponse()
        {
            var model = new AddProductDTO
            {
                Name = string.Empty,
                Category = "New Category",
                Price = 100,
                Description = "New Description"
            };

            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            var result = await _controller.AddProducts(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid product data.", result.Message);
            Assert.Contains("The Name field is required.", (string)result.Result);
        }

        [Fact]
        public async Task AddProducts_ExceptionThrown_ReturnsErrorResponse()
        {
            var model = new AddProductDTO
            {
                Name = "New Product",
                Category = "New Category",
                Price = 100,
                Description = "New Description"
            };

            _mockData.Setup(service => service.AddProductAsync(model))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.AddProducts(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while adding the product.", result.Message);
            Assert.Equal("Database error", result.Result);
        }
    }
}
