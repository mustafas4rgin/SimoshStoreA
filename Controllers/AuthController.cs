using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAuthRepository _authRepository;

        // Bağımlılıklar interface türünde olmalı
        public AuthController(IAuthService authService, IAuthRepository authRepository)
        {
            _authService = authService;
            _authRepository = authRepository;
        }
        [HttpGet("login")]
        public IActionResult LoginForm()
        {
            // Genellikle bir login formu döneriz. 
            // Ancak burada sadece örnek olarak dönüş yapıyoruz.
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel loginRequest)
        {
            var user = _authService.ValidatedUser(loginRequest);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var token = _authService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
