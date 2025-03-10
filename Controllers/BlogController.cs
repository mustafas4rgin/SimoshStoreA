using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;

namespace MyApp.Namespace
{
    public class BlogController(IHttpClientFactory httpClientFactory) : BaseController
    {
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
        
        public async Task<IActionResult> BlogPost(int id)
        {
            var response = await Client.GetAsync($"/api/blog-post/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var model = await response.Content.ReadFromJsonAsync<BlogPostViewModel>();
            
            return View(model);

        }
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var response = await Client.DeleteAsync($"/api/delete/blog/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }
            return RedirectToAction("ListBlogs","Admin");
        }
        public async Task<IActionResult> BlogList()
        {
            var response = await Client.GetAsync("/api/blogs");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var blogs = await response.Content.ReadFromJsonAsync<List<BlogEntity>>();

            return View(blogs);
        }
        // public async Task<IActionResult> BlogListWithTag(BlogTagEntity blogTagEntity)
        // {
        //     return RedirectToAction("UnderConstruction", "Error");
        // }
        // public async Task<IActionResult> RecentBlogs()
        // {
        //     var blogEntities = await _blogService.GetRecentBlogs();
        //     return View(blogEntities);
        // }
        // public async Task<IActionResult> CreateBlog()
        // {
        //     int userId = _userService.GetUserId();
        //     var user = await _userService.GetUserByIdAsync(userId);
        //     if (user is null)
        //     {
        //         return RedirectToAction("Login", "Account");
        //     }
        //     return View(new BlogDTO{
        //         userId = userId
        //     });
        // }
        // [HttpPost]
        // public async Task<IActionResult> CreateBlog(BlogDTO blogDTO)
        // {
        //     var blog = await _blogService.CreateBlogAsync(blogDTO);
        //     return RedirectToAction("BlogList");
        // }
    }
}
