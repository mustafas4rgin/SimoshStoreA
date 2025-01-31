namespace SimoshStore;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        //TODO: Service katmanı register işlemleri yapılacak
        return services;
    }
}
