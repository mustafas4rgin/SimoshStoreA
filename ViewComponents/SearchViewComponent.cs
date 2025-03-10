using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;
using System.Linq;
using System.Threading.Tasks;

public class SearchViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private HttpClient Client => httpClientFactory.CreateClient("Api.Data");

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var response = await Client.GetAsync("/api/search-product");

        if(!response.IsSuccessStatusCode)
        {
            return View(new SearchBarViewModel());
        }

        var model = await response.Content.ReadFromJsonAsync<SearchBarViewModel>();

        return View(model);
    }
}
