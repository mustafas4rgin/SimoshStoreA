using System.Threading.Tasks;
using App.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IDataRepository _Repository;
        public ProductController(IProductService productService, IDataRepository repository)
        {
            _Repository = repository;
            _productService = productService;
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _Repository.GetAll<ProductEntity>().ToListAsync();
            var result = await _productService.GetAllProductsAsync();
            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            return View(products);
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _Repository.GetByIdAsync<ProductEntity>(id);
            var result = await _productService.GetProductAsync(product);
            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            return View(product);
        }
        [HttpGet("CreateProduct")]
        [Authorize(Roles = "Admin, Seller")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var result = await _productService.CreateProductAsync(productDTO);
            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            return RedirectToAction("GetProducts");
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _Repository.GetByIdAsync<ProductEntity>(id);
            if (product is null)
            {
                return NotFound();
            }
            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Price = product.Price,
                SellerId = product.SellerId,
                CategoryId = product.CategoryId,
                DiscountId = product.DiscountId,
                Description = product.Description,
                StockAmount = product.StockAmount,
                Enabled = product.Enabled
            };
            return View(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            var result = await _productService.UpdateProductAsync(productDTO, id);
            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            return RedirectToAction("GetProducts");
        }
            [HttpPost("DeleteProduct")]
            public async Task<IActionResult> DeleteProduct(int id)
            {
                var result = await _productService.DeleteProductAsync(id);
                if (!result.Success)
                {
                    ViewBag.Error = result.Message;
                    return View();
                }
                return RedirectToAction("GetProducts");
            }
            
        }
    }
