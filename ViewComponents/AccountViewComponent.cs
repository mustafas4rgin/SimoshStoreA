using System.Security.Claims;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SimoshStore;

public class AccountViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = GetUserId();
        
        var response = await Client.GetAsync($"/api/users/{userId}");

        if(!response.IsSuccessStatusCode)
        {
            return Content("Data cannot be fetched.");
        }

        var user = await response.Content.ReadFromJsonAsync<UserEntity>();
        
        return View(user);
    }
    private int? GetUserId()
        {
            return int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : null;
        }
}
