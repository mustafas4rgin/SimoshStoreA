using SimoshStore;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>(); // Burada IAuthService ile AuthService kaydediliyor
        services.AddScoped<IAuthRepository, AuthRepository>(); // AuthRepository kaydedildiğinden emin olun
        return services;
    }
}
