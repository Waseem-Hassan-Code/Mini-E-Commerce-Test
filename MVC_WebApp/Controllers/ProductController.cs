using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MVC_WebApp.Model;
using MVC_WebApp.Services.IServices;
using Newtonsoft.Json;

namespace MVC_WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly INotyfService _notyf;

        public ProductController(IProductService product, INotyfService notyf)
        {
            _product = product;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts(string? searchTerm)
        {
            try
            {
                List<ProductDto>? list = new();

                ResponseDto? response = await _product.GetProductsAsync(searchTerm);

                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        _notyf.Success(response?.Message, 4);
                    }
                }
                else
                {
                    _notyf.Error(response?.Message, 4);
                }

                return Json(new
                {
                    data = list
                });
            }
            catch (Exception ex)
            {
                _notyf.Error("An error occurred while fetching products.", 4);
                return Json(new { data = new List<ProductDto>(), error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] AddProductDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseDto? response = await _product.AddProductAsync(model);

                    if (response != null && response.IsSuccess)
                    {
                        _notyf.Success(response?.Message, 4);
                        return Json(new { success = true });
                    }
                    else
                    {
                        _notyf.Error(response?.Message, 4);
                    }
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _notyf.Error("An error occurred while creating the product.", 4);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct([FromForm] UpdateProductDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseDto? response = await _product.UpdateProductAsync(model);

                    if (response != null && response.IsSuccess)
                    {
                        _notyf.Success(response?.Message, 4);
                        return Json(new { success = true });
                    }
                    else
                    {
                        _notyf.Error(response?.Message, 4);
                    }
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _notyf.Error("An error occurred while updating the product.", 4);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }

                ResponseDto? response = await _product.DeleteProductAsync(id);

                if (response != null && response.IsSuccess)
                {
                    _notyf.Success(response?.Message, 4);
                    return Json(new { success = true });
                }
                else
                {
                    _notyf.Error(response?.Message, 4);
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _notyf.Error("An error occurred while deleting the product.", 4);
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
