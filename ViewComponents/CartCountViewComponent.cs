using System.Security.Claims;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Namespace;

namespace SimoshStore;

public class CartCountViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = GetUserId();
        int count = 0;
        if (userId is null)
        {
            return View(0);
        }

        var response = await Client.GetAsync($"/api/{userId}/cart-items");

        if(!response.IsSuccessStatusCode)
        {
            return Content("Data cannot be fetched.");
        }

        var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemEntity>>();
        
        foreach(var item in cartItems)
        {
            count += item.Quantity;
        }
        return View(count);
    }
     private int? GetUserId()
        {
            return int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : null;
        }
}
