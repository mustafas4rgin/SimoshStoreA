using System.Threading.Tasks;
using App.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;

namespace MyApp.Namespace
{
    [Authorize(Roles = "admin")]
    public class ProductController(IHttpClientFactory httpClientFactory, ApiHelper apiHelper) : BaseController
    {
        ApiHelper _apiHelper => apiHelper;
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await Client.GetAsync("/api/products");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            var products = await response.Content.ReadFromJsonAsync<List<ProductEntity>>();

            return View("ProductList", products);
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await Client.GetAsync($"/api/products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            var product = await response.Content.ReadFromJsonAsync<ProductEntity>();

            return View(product);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProduct()
        {
            var request = await CreateRequestMessage("/api/categories");

            var response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            var categories = await response.Content.ReadFromJsonAsync<List<CategoryEntity>>();

            ViewBag.Categories = categories;

            return View(new ProductDTO());
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
             var response = await apiHelper.SendApiRequestAsync("/api/create/product", HttpMethod.Post, productDTO);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }
            return RedirectToAction("ProductList", "Admin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            
            var response = await Client.GetAsync($"/api/update-admin/product/{id}");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            var dto = await response.Content.ReadFromJsonAsync<UpdateProductDTO>();

            if (dto == null)
            {
                return NotFound();
            }

            var categories = dto.Categories;
            var discounts = dto.Discounts;

            var productDTO = new ProductDTO
            {
                SellerId = dto.Product.SellerId,
                CategoryId = dto.Product.CategoryId,
                DiscountId = dto.Product.DiscountId,
                Name = dto.Product.Name,
                Price = dto.Product.Price,
                Description = dto.Product.Description,
                StockAmount = dto.Product.StockAmount,
                Enabled = dto.Product.Enabled
            };

            ViewBag.Categories = categories;
            ViewBag.Discounts = discounts;

            return View(productDTO);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            var response = await apiHelper.SendApiRequestAsync($"/api/update/product/{id}", HttpMethod.Put, productDTO);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            SetSuccessMessage("Product uptaded successfully.");

            return RedirectToAction("AdminDashboard", "Admin");

        }
        [Authorize(Roles="admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
            var response = await apiHelper.SendDeleteRequestAsync("/api/delete/product", HttpMethod.Delete, id);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            SetSuccessMessage("Product deleted successfully.");

            return RedirectToAction("AdminDashboard", "Admin");
        }


    }
}
