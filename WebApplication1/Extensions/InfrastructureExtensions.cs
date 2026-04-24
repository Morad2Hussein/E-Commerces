



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_CommerceWeb.Extensions
{
    public static class InfrastructureExtensions
    {
        #region AddInfrastructure
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!, true);
                configurationOptions.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(configurationOptions);
            });

            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddIdentity<User, IdentityRole>(
                option =>
                {
                    option.Password.RequireNonAlphanumeric = true;
                    option.Password.RequireDigit = true;
                    option.Password.RequireUppercase = true;
                    option.Password.RequireLowercase = true;
                    option.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<IdentityStoreDbContext>();
            services.ValidateJwt(configuration);
            return services;
        }
        #endregion
        #region Validation JWT Options 
        public static IServiceCollection ValidateJwt(this IServiceCollection services , IConfiguration configuration)
        {
            var JwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            #region Add AddAuthentication
            services.AddAuthentication(options =>
                {
                    // check if the user is authenticated [LogIn] or not
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    // check if the user is authorized to access the resource or not
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = JwtOptions.Issuer,
                            ValidAudience = JwtOptions.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey))
                        };
                    }
                    );
            #endregion
            services.AddAuthorization();

            return services;



        }
        #endregion
    }
}

