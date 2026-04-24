namespace E_CommerceWeb.Extensions
{
    public static class WebApplictionRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pandingMigrations = await dbContextService.Database.GetPendingMigrationsAsync();
            if (pandingMigrations.Any())
                await dbContextService.Database.MigrateAsync();
            
            return app;
        }
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            await DataInitializerService.InitializeAsync();
             await  DataInitializerService.SeedIdentityDataAsync();
            return app;

        }

    }
}
