namespace E_CommerceWeb.Extensions
{
    public static class WepApiServicesExtensions
    {
        public static IServiceCollection AddWepServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddControllers();
            var frontUrl = configuration.GetSection("URLS")["FrontURL"];
            ;
            services.AddCors(
                options =>
                {
                    options.AddPolicy("CorsPolicy", policy =>
                    {
                        policy.AllowAnyHeader() 
                              .AllowAnyMethod()
                              .WithOrigins(frontUrl!); 
                    });
                });
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            return services;
        }
    }
}
