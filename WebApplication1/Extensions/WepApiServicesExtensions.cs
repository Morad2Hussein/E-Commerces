namespace E_CommerceWeb.Extensions
{
    public static class WepApiServicesExtensions
    {
        public static IServiceCollection AddWepServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            return services;
        }
    }
}
