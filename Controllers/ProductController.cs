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
    public class ProductController(IHttpClientFactory httpClientFactory) : BaseController
    {
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await Client.GetAsync("/api/products");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var products = await response.Content.ReadFromJsonAsync<List<ProductEntity>>();

            return View("ProductList", products);
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await Client.GetAsync($"/api/products/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var product = await response.Content.ReadFromJsonAsync<ProductEntity>();

            return View(product);
        }
        public async Task<IActionResult> CreateProduct()
        {
            var response = await Client.GetAsync("/api/categories");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var categories = await response.Content.ReadFromJsonAsync<List<CategoryEntity>>();

            ViewBag.Categories = categories;

            return View(new ProductDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var response = await Client.PostAsJsonAsync("/api/create/product", productDTO);

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }
            return RedirectToAction("ProductList", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var response = await Client.GetAsync($"/api/update-admin/product/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
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

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            var response = await Client.PutAsJsonAsync($"/api/update/product/{id}",productDTO);

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("Product uptaded successfully.");

            return RedirectToAction("AdminDashboard", "Admin");

        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await Client.DeleteAsync($"/api/delete/product/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("Product deleted successfully.");

            return RedirectToAction("AdminDashboard", "Admin");
        }


    }
}
