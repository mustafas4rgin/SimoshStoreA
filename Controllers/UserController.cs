using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    [Authorize(Roles = "admin")]
    public class UserController(IHttpClientFactory httpClientFactory) : BaseController
    {
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");

        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var response = await Client.DeleteAsync($"/api/delete/user/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
            }

            SetSuccessMessage("User deleted successfully.");

            return RedirectToAction("ManageUsers", "Admin");
        }
        public async Task<IActionResult> UpdateUser(int id)
        {
            var response = await Client.GetAsync($"/api/users/{id}");

            if(!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Data cannot be fetched.");
                return RedirectToAction("NotFound","Error");
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
            if (ModelState.IsValid)
            {
                var response = await Client.PutAsJsonAsync($"/api/update/user/{model.Id}",new UserDTO
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email
                });

                if(!response.IsSuccessStatusCode)
                {
                    SetErrorMessage("Data cannot be fetched.");
                    return RedirectToAction("NotFound","Error");
                }
            }

            SetSuccessMessage("User updated successfully.");
            
            return RedirectToAction("AdminDashboard","Admin");
        }

    }
}
