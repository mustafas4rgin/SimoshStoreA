using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SimoshStore;

public class FeatureBoxViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var response = await Client.GetAsync("/api/blogs");

        if(!response.IsSuccessStatusCode)
        {
            return View(new List<BlogEntity>());
        }

        var blogs = await response.Content.ReadFromJsonAsync<List<BlogEntity>>();

        return View(blogs);
    }
}
