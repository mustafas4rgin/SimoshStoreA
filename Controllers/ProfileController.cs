using System.Security.Claims;
using App.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfileService _ProfileService;
        private readonly IOrderService _OrderService;
        private readonly IUserService _UserService;
        public ProfileController(IUserService userService, IOrderService orderService, IHttpContextAccessor httpContextAccessor, IProfileService profileService)
        {
            _ProfileService = profileService;
            _httpContextAccessor = httpContextAccessor;
            _OrderService = orderService;
            _UserService = userService;
        }
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var order = await _OrderService.GetOrderByIdAsync(id);
            return View(order);


        }
        [HttpGet]
        public IActionResult UpdateProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserDTO userDTO)
        {
            int userId = _UserService.GetUserId();
            var result = await _UserService.UpdateUserAsync(userDTO, userId);
            if (!result.Success)
            {
                ViewData["Error"] = result.Message;
                return View();
            }
            return RedirectToAction("MyProfile");
        }
        public async Task<IActionResult> MyProfileAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            if (userIdClaim == null)
            {
                ViewData["AuthError"] = "You need to login to view this page";
                return RedirectToAction("Login", "Auth");
            }
            int userId = int.Parse(userIdClaim);
            var user = await _ProfileService.GetUserByIdAsync(userId);
            var comments = await _ProfileService.GetComments();
            var orders = await _OrderService.GetOrdersByUserIdAsync(userId);
            var ordersItems = await _OrderService.GetAllOrderItems();
            return View(new ProfileViewModel
            {
                 orderItems = ordersItems.ToList(),
                user = user,
                productComments = comments.ToList(),
                orders = orders.ToList()
            });
        }
        public async Task<IActionResult> Address()
        {
            var userId = _UserService.GetUserId();
            var user = await _ProfileService.GetUserByIdAsync(userId);
            return View(user);
        }
        [HttpGet]
        public IActionResult EditAddress()
        {
            var countries = AddressHelper.GetCountries();
            var cities = AddressHelper.GetCities();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditAddress(EditAddressViewModel model)
        {
            var userId = _UserService.GetUserId();
            var user = await _ProfileService.GetUserByIdAsync(userId);
            var result = await _UserService.UpdateUserAddress(model);
            if (!result.Success)
            {
                ViewData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
