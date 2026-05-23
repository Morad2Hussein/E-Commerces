

namespace E_CommerceWeb.Extensions
{
    public static class CoreServicesExtensions
{
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
            {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReferenceMapping).Assembly);
            services.AddScoped<IServicesManager, ServicesManagerWithFactoryDelegate>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<ICacheSerices, CacheSerices>();
            services.AddScoped<Func<IProductServices>>
                (provider => () => provider.GetRequiredService<IProductServices>()
                );
            services.AddScoped<Func<IBasketServices>> (
                provider => () => provider.GetRequiredService<IBasketServices>()
                );
            services.AddScoped<Func<IAuthenticationService>>(
                provider => () => provider.GetRequiredService<IAuthenticationService>()
                );
            services.AddScoped<Func<IOrderServices>>(
                provider => () => provider.GetRequiredService<IOrderServices>()
                );
            services.AddScoped<Func<IPaymentServices>>(
                provider => () => provider.GetRequiredService<IPaymentServices>()
                );
            services.AddScoped<Func<ICacheSerices>>(
                provider => () => provider.GetRequiredService<ICacheSerices>()
                );
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
 