
namespace E_CommerceWeb.Extensions
{
    public static class CoreServicesExtensions
{
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
            {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReferenceMapping).Assembly);
            services.AddScoped<IServicesManager, ServicesManager>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
 