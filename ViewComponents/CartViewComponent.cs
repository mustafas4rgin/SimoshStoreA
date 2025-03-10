using System.Security.Claims;
using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace SimoshStore;

public class CartViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = GetUserId();

        if(userId is null)
        {
            return View(new List<CartItemEntity>());
        }
        var response = await Client.GetAsync($"/api/{userId}/cart-items");

        if(!response.IsSuccessStatusCode)
        {
            return View(new List<CartItemEntity>());
        }

        var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemEntity>>();


        if (userId == null)
        {
            return View(new List<CartItemEntity>());
        }
        if (cartItems.Count== 0)
            return View(new List<CartItemEntity>());
        return View(cartItems);
    }
    private int? GetUserId()
        {
            return int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : null;
        }
}
