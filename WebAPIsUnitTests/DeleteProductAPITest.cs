using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using WebAPIs.Controllers;
using DAL.Services.IServices;
using MVC_WebApp.Model;

namespace WebAPIsUnitTests
{
    public class DeleteProductAPITest
    {
        private readonly Mock<IProductsData> _mockData;
        private readonly ProductController _controller;

        public DeleteProductAPITest()
        {
            _mockData = new Mock<IProductsData>();
            _controller = new ProductController(_mockData.Object);
        }

        [Fact]
        public async Task DeleteProduct_ValidId_ReturnsSuccessResponse()
        {
            int productId = 1;

            _mockData.Setup(service => service.DeleteProductAsync(productId))
                .ReturnsAsync(true);

            var result = await _controller.DeleteProduct(productId);

            Assert.True(result.IsSuccess);
            Assert.Equal("Product deleted successfully.", result.Message);
        }

        [Fact]
        public async Task DeleteProduct_InvalidId_ReturnsErrorResponse()
        {
            int productId = 0;

            var result = await _controller.DeleteProduct(productId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid product ID.", result.Message);
        }

        [Fact]
        public async Task DeleteProduct_ProductNotFound_ReturnsErrorResponse()
        {
            int productId = 1;

            _mockData.Setup(service => service.DeleteProductAsync(productId))
                .ReturnsAsync(false);

            var result = await _controller.DeleteProduct(productId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Product not found.", result.Message);
        }

        [Fact]
        public async Task DeleteProduct_ExceptionThrown_ReturnsErrorResponse()
        {
            int productId = 1;

            _mockData.Setup(service => service.DeleteProductAsync(productId))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.DeleteProduct(productId);

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while deleting the product.", result.Message);
            Assert.Equal("Database error", result.Result);
        }
    }
}
