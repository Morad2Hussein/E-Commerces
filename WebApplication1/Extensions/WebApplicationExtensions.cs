namespace E_CommerceWeb.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedingDatabaseAsync(this WebApplication app)
        {

            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync();
            return app;
        }
        public static WebApplication AddExceptionsHandleMiddleWares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleWare>();
            return app;

        }
        public static WebApplication AddSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
