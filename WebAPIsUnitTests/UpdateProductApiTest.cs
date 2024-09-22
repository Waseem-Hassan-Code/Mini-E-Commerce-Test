using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using WebAPIs.Controllers;
using DAL.Services.IServices;
using MVC_WebApp.Model;
using DAL.DTOs;

namespace WebAPIsUnitTests
{
    public class UpdateProductApiTest
    {
        private readonly Mock<IProductsData> _mockData;
        private readonly ProductController _controller;

        public UpdateProductApiTest()
        {
            _mockData = new Mock<IProductsData>();
            _controller = new ProductController(_mockData.Object);
        }

        [Fact]
        public async Task UpdateProduct_ValidModel_ReturnsSuccessResponse()
        {
            var model = new UpdateProductDTO
            {
                Id = 1,
                Name = "Updated Product",
                Category = "Updated Category",
                Price = 150,
                Description = "Updated Description"
            };

            _mockData.Setup(service => service.UpdateProductAsync(model))
                .ReturnsAsync(model);

            var result = await _controller.UpdateProduct(model);

            Assert.True(result.IsSuccess);
            Assert.Equal("Product updated successfully.", result.Message);
            Assert.Equal(model, result.Result);
        }

        [Fact]
        public async Task UpdateProduct_NullModel_ReturnsErrorResponse()
        {
            UpdateProductDTO model = null;

            var result = await _controller.UpdateProduct(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("Product data must be provided.", result.Message);
        }

        [Fact]
        public async Task UpdateProduct_ModelStateInvalid_ReturnsErrorResponse()
        {
            var model = new UpdateProductDTO
            {
                Id = 1,
                Name = "",
                Category = "Category",
                Price = 100,
                Description = "Description"
            };

            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            var result = await _controller.UpdateProduct(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid product data.", result.Message);
            Assert.Contains("The Name field is required.", (string)result.Result);
        }

        [Fact]
        public async Task UpdateProduct_ProductNotFound_ReturnsErrorResponse()
        {
            var model = new UpdateProductDTO
            {
                Id = 1,
                Name = "Nonexistent Product",
                Category = "Category",
                Price = 100,
                Description = "Description"
            };

            _mockData.Setup(service => service.UpdateProductAsync(model))
                .ThrowsAsync(new Exception("Product not found."));

            var result = await _controller.UpdateProduct(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while updating the product.", result.Message);
            Assert.Equal("Product not found.", result.Result);
        }

        [Fact]
        public async Task UpdateProduct_ExceptionThrown_ReturnsErrorResponse()
        {
            var model = new UpdateProductDTO
            {
                Id = 1,
                Name = "Test Product",
                Category = "Category",
                Price = 100,
                Description = "Description"
            };

            _mockData.Setup(service => service.UpdateProductAsync(model))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.UpdateProduct(model);

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while updating the product.", result.Message);
            Assert.Equal("Database error", result.Result);
        }
    }
}
