using System.Security.Claims;
using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;

namespace MyApp.Namespace
{
    public class CartController(IHttpClientFactory httpClientFactory) : BaseController
    {
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
        public async Task<IActionResult> ShopCart()
        {
            var userId = GetUserId();

            var response = await Client.GetAsync($"/api/{userId}/cart-items");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<CartItemEntity>());
            }

            var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemEntity>>();

            return View(cartItems);

        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = GetUserId();

            if(userId == null)
            {
                SetErrorMessage("You must login.");
                return RedirectToAction("NotFound","Error");
            }

            var response = await Client.PostAsJsonAsync($"/api/add-to-cart/{userId}",new CartDTO
            {
                ProductId = productId,
                Quantity = quantity,
            });

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("Cart item added successfully.");

            return RedirectToAction("ProductList", "Shop");
        }




        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = GetUserId();

            if(userId == null)
            {
                SetErrorMessage("You must login.");
                return RedirectToAction("NotFound","Error");
            }
            var response = await Client.PutAsJsonAsync($"/api/remove-from-cart/{userId}",productId);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("Cart item removed.");

            return RedirectToAction("ProductList", "Shop");
        }
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            if(userId is null)
            {
                SetErrorMessage("You must login.");
                return RedirectToAction("NotFound","Error");
            }

            var response = await Client.DeleteAsync($"/api/clear-cart/{userId}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("Cart cleared successfully.");

            return RedirectToAction("ShopCart", "Cart");
        }

        // [HttpPost]
        // public async Task<IActionResult> UpdateCart(int productId, int quantity)
        // {
        //     if (quantity <= 0)
        //     {
        //         return BadRequest("Geçersiz quantity.");
        //     }

        //     var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
        //         .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

        //     if (userIdClaim == null)
        //     {
        //         return Unauthorized();
        //     }

        //     var userId = int.Parse(userIdClaim);

        //     var cartItem = await _Repository.GetAll<CartItemEntity>()
        //         .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);

        //     if (cartItem == null)
        //     {
        //         return NotFound("Ürün sepette bulunamadı.");
        //     }

        //     cartItem.Quantity = quantity;
        //     await _Repository.UpdateAsync(cartItem);

        //     return RedirectToAction("Cart", "Cart");
        // }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var userId = GetUserId();

            if(userId is null)
            {
                SetErrorMessage("You must login");
                return RedirectToAction("NotFound","Error");
            }

            var response = await Client.GetAsync($"/api/order-checkout/{userId}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var model = await response.Content.ReadFromJsonAsync<CheckOutViewModel>();
            
            return View(model);

        }
        // [HttpPost]
        // public async Task<IActionResult> CheckOut(CheckOutViewModel model)
        // {
        //     var UserId = GetUserId(); 
        //     if (UserId == null)
        //     {
        //         SetErrorMessage("You must login.");
        //         return RedirectToAction("NotFound", "Error"); 
        //     }

        //     var response = await Client.PostAsJsonAsync("/api/order-checkout",model);

        //     if(!response.IsSuccessStatusCode)
        //     {
        //         SetErrorMessage("Data cannot be fetched.");
        //         return RedirectToAction("NotFound","Error");
        //     }
        //     return RedirectToAction("CreateOrder", "Order", new { userId = UserId});
        // }

    }

}
