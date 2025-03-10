using Microsoft.AspNetCore.Authentication.Cookies;
using SimoshStore;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name ="auth-cookie";
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.SlidingExpiration = true;
            options.LoginPath ="/Login";
            options.LogoutPath ="/Logout";
            options.AccessDeniedPath = "/AccessDenied";
        });

        services.AddHttpContextAccessor();

        
        return services;
    }
}
