using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    
    public class UserController(IHttpClientFactory httpClientFactory, ApiHelper apiHelper) : BaseController
    {
        private ApiHelper _apiHelper => apiHelper;
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
        
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var response = await _apiHelper.SendDeleteRequestAsync($"/api/delete/user",HttpMethod.Delete,id);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            SetSuccessMessage("User deleted successfully.");

            return RedirectToAction("ManageUsers", "Admin");
        }
        [Authorize(Roles = "admin,buyer,seller")]
        public async Task<IActionResult> UpdateUser(int id)
        {

            var response = await Client.GetAsync($"/api/users/{id}");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound", "Error");
            }

            var user = await response.Content.ReadFromJsonAsync<UserEntity>();

            var viewModel = new UpdateUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                Enabled = user.Enabled,
                Address = user.Address
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserViewModel model)
        {
            var userId = GetUserId();

            if (ModelState.IsValid)
            {
                var response = await apiHelper.SendApiRequestAsync($"/api/update/user/{userId}", HttpMethod.Put, new UserDTO
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                });

                if (!response.IsSuccessStatusCode)
                {
                    SetErrorMessage("Data cannot be fetched.");
                    return RedirectToAction("NotFound", "Error");
                }
            }

            SetSuccessMessage("User updated successfully.");

            return RedirectToAction("AdminDashboard", "Admin");
        }

    }
}
