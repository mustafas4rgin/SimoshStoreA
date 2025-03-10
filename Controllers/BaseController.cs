using System.Net.Http.Headers;
using System.Security.Claims;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public abstract class BaseController : Controller
    {
        protected T? GetService<T>() where T : class => HttpContext.RequestServices.GetService<T>();

        protected string GetToken() => HttpContext.Session.GetString("JwtToken") ?? "";

        protected void SetSuccessMessage(string message) => ViewBag.SuccessMessage = message;

        protected void SetErrorMessage(string message) => ViewBag.ErrorMessage = message;

        protected string? GetCookie(string key) => Request.Cookies[key];

        protected void SetCookie(string key, string value) => Response.Cookies.Append(key, value);

        protected void RemoveCookie(string key) => Response.Cookies.Delete(key);

        protected async Task<HttpRequestMessage> CreateRequestMessage(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var token = GetToken();  

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return request;
        }
        protected async Task<UserEntity?> GetCurrentUserAsync()
        {
            var userId = GetUserId();

            if (userId is null)
            {
                return null;
            }

            var clientFactory = GetService<IHttpClientFactory>();

            if (clientFactory == null)
            {
                return null;
            }

            var client = clientFactory.CreateClient("Api.Data");

            if (client == null)
            {
                return null;
            }

            var response = await client.GetAsync($"api/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<UserEntity>();

            if (user == null)
            {
                return null;
            }

            return user;
        }
        
        protected bool IsUserLoggedIn() => User.Identity?.IsAuthenticated ?? false;

        protected int? GetUserId() => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : null;
    }
}
