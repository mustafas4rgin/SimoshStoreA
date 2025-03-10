using System.Security.Claims;
using System.Threading.Tasks;
using App.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    public class ProfileController(IHttpClientFactory httpClientFactory, ApiHelper apiHelper) : BaseController
    {
        private ApiHelper _apiHelper => apiHelper;
        private HttpClient Client => httpClientFactory.CreateClient("Api.Data");
        public async Task<IActionResult> Review()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                SetErrorMessage("You need to login to view this page");
                return RedirectToAction("NotFound", "Error");
            }

            var response = await Client.GetAsync($"api/productComments/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Failed to load reviews");
                return RedirectToAction("NotFound", "Error");
            }

            var comments = await response.Content.ReadFromJsonAsync<List<ProductCommentEntity>>();

            return View(comments);
        }
        [Authorize]
        public async Task<IActionResult> UpdateProfileAsync()
        {
            var userId = GetUserId();

            if(userId == null)
            {
                SetErrorMessage("You must login");
                return RedirectToAction("NotFound","Error");
            }

            var response = await Client.GetAsync($"api/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Failed to load user");
                return RedirectToAction("NotFound", "Error");
            }

            var user = await response.Content.ReadFromJsonAsync<UserEntity>();
            return View(
                new UpdateProfileViewModel
                {
                    user = user,

                }
            );
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please fill in all fields");
                return View();
            }
            var userId = GetUserId();

            if(userId == null)
            {
                SetErrorMessage("You must login");
                return RedirectToAction("NotFound","Error");
            }

            if(userId == model.user.Id)
            {
                SetErrorMessage("You are not authorized");
                return RedirectToAction("NotFound","Error");
            }

            var dto = new UserDTO
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
            };

            var response = await _apiHelper.SendApiRequestAsync($"/api/update/user/{userId}", HttpMethod.Put, dto);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Failed to update user");
                return RedirectToAction("NotFound", "Error");
            }

            SetSuccessMessage("Profile updated successfully");

            return RedirectToAction("MyProfile");
        }
        public async Task<IActionResult> MyProfileAsync()
        {
            var userId = GetUserId();
            
            if(userId == null)
            {
                SetErrorMessage("You must login.");
                return RedirectToAction("NotFound","Error");
            }

            var request =  await CreateRequestMessage($"/api/my-profile/{userId}");

            var response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Failed to load profile");
                return RedirectToAction("NotFound", "Error");
            }

            var profile = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
            return View(profile);
        }
        [HttpGet("/address")]
        public async Task<IActionResult> Address()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                SetErrorMessage("You must login");
                return RedirectToAction("NotFound", "Error");
            }

            var response = await Client.GetAsync($"api/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Failed to load user");
                return RedirectToAction("NotFound", "Error");
            }

            var user = await response.Content.ReadFromJsonAsync<UserEntity>();

            return View(user);
        }
        [HttpGet]
        public IActionResult EditAddress()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditAddress(EditAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please fill in all fields");
                return View(model);
            }
            var userId = GetUserId();

            if (userId == null)
            {
                SetErrorMessage("You must login");
                return RedirectToAction("NotFound", "Error");
            }
            
            var response = await _apiHelper.SendApiRequestAsync("/api/update/address",HttpMethod.Put,model);

            if (!response.IsSuccessStatusCode)
            {
                SetErrorMessage("Failed to update address");
                return RedirectToAction("NotFound", "Error");
            }
            
            SetSuccessMessage("Address updated successfully");

            return RedirectToAction("Address");
        }
    }
}
