using System.Security.Claims;
using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;

namespace MyApp.Namespace
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataRepository _Repository;
        public CartController(IDataRepository Repository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _Repository = Repository;
        }
        
        public async Task<IActionResult> AddToCart(int productId, int quantity)
{
    // quantity'yi kontrol edelim
    if (quantity == 0)
    {
        return BadRequest("Geçersiz quantity değeri.");
    }

    var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

    if (userIdClaim == null)
    {
        return Unauthorized();
    }

    var userId = int.Parse(userIdClaim);

    var product = await _Repository.GetByIdAsync<ProductEntity>(productId);

    if (product == null)
    {
        return NotFound();
    }

    // Sepette zaten var mı kontrol et
    var existingCartItem = await _Repository.GetAll<CartItemEntity>()
        .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);

    if (existingCartItem != null)
    {
        // Eğer ürün sepette varsa, miktarını artır
        existingCartItem.Quantity += quantity;
        await _Repository.UpdateAsync(existingCartItem);
    }
    else
    {
        // Eğer ürün sepette yoksa, yeni bir öğe ekle
        var cartItem = new CartItemEntity
        {
            Quantity = quantity,
            ProductId = productId,
            UserId = userId
        };
        await _Repository.AddAsync(cartItem);
    }

    var refererUrl = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();
    if (!string.IsNullOrEmpty(refererUrl))
    {
        return Redirect(refererUrl);
    }

    return RedirectToAction("ProductList", "Shop");
}




        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim);
            var cartItem = _Repository.GetAll<CartItemEntity>()
            .FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            if (cartItem == null)
            {
                return NotFound();
            }
            await _Repository.DeleteAsync<CartItemEntity>(cartItem.Id);
            return RedirectToAction("ProductList", "Shop");
        }
        public async Task<IActionResult> ClearCart(int productId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim);
            var cartItems = _Repository.GetAll<CartItemEntity>()
            .Where(c => c.UserId == userId);
            foreach (var cartItem in cartItems)
            {
                await _Repository.DeleteAsync<CartItemEntity>(cartItem.Id);
            }
            return Ok();
        }
        [HttpPost]
public async Task<IActionResult> UpdateCart(int productId, int quantity)
{
    if (quantity <= 0)
    {
        return BadRequest("Geçersiz quantity.");
    }

    var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

    if (userIdClaim == null)
    {
        return Unauthorized();
    }

    var userId = int.Parse(userIdClaim);

    var cartItem = await _Repository.GetAll<CartItemEntity>()
        .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);

    if (cartItem == null)
    {
        return NotFound("Ürün sepette bulunamadı.");
    }

    cartItem.Quantity = quantity;
    await _Repository.UpdateAsync(cartItem);

    return RedirectToAction("Cart", "Cart");
}

        public IActionResult GetCartItemCount()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (userIdClaim == null)
            {
                return Json(0); // Kullanıcı giriş yapmamışsa, 0 döndürüyoruz.
            }

            int userId = int.Parse(userIdClaim);
            var cartItems = _Repository.GetAll<CartItemEntity>().Where(x => x.UserId == userId).ToList();
            int cartCount = cartItems.Count();

            return Json(cartCount); // JSON olarak döndürülüyor
        }


    }
    

}
