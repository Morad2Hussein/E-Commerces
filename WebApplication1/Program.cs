

namespace WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Services
            builder.Services.AddWepServices(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion

            #region Middlewares

            var app = builder.Build();
            // Data Seeding 
            await app.SeedingDatabaseAsync();
            // Global Exception Handling MiddleWare
            app.AddExceptionsHandleMiddleWares();
            if (app.Environment.IsDevelopment())
            {
                app.AddSwaggerMiddleWares();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync(); 
            #endregion
        }
    }
}
