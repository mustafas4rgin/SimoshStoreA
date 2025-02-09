using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace SimoshStore;

public interface IAuthService
{
    string GenerateToken(User user);
    ClaimsPrincipal ValidateToken(string token);
    User ValidatedUser([FromBody] LoginViewModel loginRequest);
}
