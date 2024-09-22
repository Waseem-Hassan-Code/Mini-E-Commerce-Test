using DAL.Models;
using DAL.Services.IServices;
using Moq;
using MVC_WebApp.Model;
using WebAPIs.Controllers;
using Xunit;

namespace WebAPIsUnitTests
{
    public class GetProductsApiTest
    {
        private readonly Mock<IProductsData> _mockData;
        private readonly ProductController _controller;

        public GetProductsApiTest()
        {
            _mockData = new Mock<IProductsData>();
            _controller = new ProductController(_mockData.Object);
        }
        [Fact]
        public async Task GetProducts_ValidSearchTerm_ReturnsSuccessResponse()
        {
            var searchTerm = "test";
            var products = new List<Product>
    {
        new Product {
            Id = 1,
            Name = "Test Product",
            Category = "Test Category",
            Price = 100,
            Description = "Test Description",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
        }
    };

            _mockData.Setup(service => service.GetProductsAsync(searchTerm))
                .ReturnsAsync(products);

            var result = await _controller.GetProducts(searchTerm);

            Assert.True(result.IsSuccess);
            Assert.Equal("Products retrieved successfully.", result.Message);
            Assert.Equal(products, result.Result); 
        }

        [Fact]
        public async Task GetProducts_EmptySearchTerm_ReturnsSuccessResponseWithEmptyResult()
        {
            var searchTerm = string.Empty;

            var result = await _controller.GetProducts(searchTerm);

            Assert.True(result.IsSuccess);
            Assert.Equal("Products retrieved successfully.", result.Message);
            Assert.Empty(result.Result as IEnumerable<ProductDto>);
        }


        [Fact]
        public async Task GetProducts_SearchTermTooLong_ReturnsErrorResponse()
        {
            var searchTerm = new string('a', 31);

            var result = await _controller.GetProducts(searchTerm);

            Assert.False(result.IsSuccess);
            Assert.Equal("Input parameters are too long.", result.Message);
        }

        [Fact]
        public async Task GetProducts_NoProductsFound_ReturnsErrorResponse()
        {
            var searchTerm = "nonexistent";
            var products = new List<Product>();

            _mockData.Setup(service => service.GetProductsAsync(searchTerm))
                .ReturnsAsync(products);

            var result = await _controller.GetProducts(searchTerm);

            Assert.False(result.IsSuccess);
            Assert.Equal("No products found matching the criteria.", result.Message);
        }

        [Fact]
        public async Task GetProducts_ExceptionThrown_ReturnsErrorResponse()
        {
            var searchTerm = "test";

            _mockData.Setup(service => service.GetProductsAsync(searchTerm))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.GetProducts(searchTerm);

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while retrieving products.", result.Message);
            Assert.Equal("Database error", result.Result);
        }


    }
}