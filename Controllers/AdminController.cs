using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Namespace;

namespace SimoshStore.Controllers
{
    [Authorize(Roles = "admin")] 
    public class AdminController(IHttpClientFactory clientFactory) : BaseController
    {
        private HttpClient Client => clientFactory.CreateClient("Api.Data");
        
        public async Task<IActionResult> AdminDashboard()
        {
            var response = await Client.GetAsync("/api/admin-dashboard");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var model =await response.Content.ReadFromJsonAsync<AdminDashboardViewModel>();
            return View(model);
        }
        public async Task<IActionResult> OrderList()
        {
            var response = await Client.GetAsync("/api/orders");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var orders = await response.Content.ReadFromJsonAsync<List<OrderEntity>>();

            return View(orders);
        }
        public async Task<IActionResult> BlogCommentList()
        {
            var response = await Client.GetAsync("/api/blogcomments");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var comments = await response.Content.ReadFromJsonAsync<List<ProductCommentEntity>>();

            return View(comments);
        }
        public async Task<IActionResult> ManageUsersAsync(int page = 1, int pageSize = 10)
        {
            var response = await Client.GetAsync("/api/users");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var users = await response.Content.ReadFromJsonAsync<List<UserEntity>>();

            var totalUsers = users.Count();

            var viewModel = new UserListViewModel
            {
                Users = users.Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    RoleId = user.RoleId,
                    Enabled = user.Enabled
                }).ToList(),
                TotalUsersCount = totalUsers,
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(viewModel);
        }
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var response = await Client.DeleteAsync($"/api/delete/order/{orderId}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("Order deleted successfully.");

            return RedirectToAction("OrderList", "Admin");
        }

        public IActionResult ViewReports()
        {
            return View("UnderConstruction", "Error");
        }
        public async Task<IActionResult> ProductCommentList()
        {
            var response = await Client.GetAsync("/api/productcomments");
            
            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var comments = await response.Content.ReadFromJsonAsync<List<ProductCommentEntity>>();

            return View(comments);
        }
        public async Task<IActionResult> ProductList()
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
        public async Task<IActionResult> CategoryList()
        {
            var response = await Client.GetAsync("/api/categories");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }
            
            var categories = await response.Content.ReadFromJsonAsync<List<CategoryEntity>>();

            return View(categories);
        }
        public async Task<IActionResult> ListBlogCategories()
        {
            var response = await Client.GetAsync("/api/blogcategories");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var blogCategories = await response.Content.ReadFromJsonAsync<List<BlogCategoryEntity>>();

            return View(blogCategories);
        }
        public async Task<IActionResult> ListBlogs()
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

        public async Task<IActionResult> OrderDetails(int id)
        {
            var response = await Client.GetAsync($"/api/orders/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            var order = await response.Content.ReadFromJsonAsync<OrderEntity>();

            return View(order);

        }
    }
}

