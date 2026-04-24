
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Presistance.Data.DataSeeding.DataInitializer
{
    public class DataInitializer(
        StoreDbContext _context ,
         UserManager<User> _userManager,
         RoleManager<IdentityRole> _roleManager
         ) : IDataInitializer
    {
       

        #region Seeding Data  
        public async Task InitializeAsync()
        {

            var HasProductTypes = await _context.ProductTypes.AnyAsync();
            var HasProductBrands = await _context.ProductBrands.AnyAsync();
            var HasProducts = await _context.Products.AnyAsync();
            var HasDeliveryMethods = await _context.DeliveryMethods.AnyAsync();
            if (HasProductTypes && HasProductBrands && HasProducts && HasDeliveryMethods)
                return;
            try
            {
                if (!HasProductBrands)
                {
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _context.ProductBrands);
                    _context.SaveChanges();
                }
                if (!HasProductTypes)
                {
                    await SeedDataFromJsonAsync<ProductType, int>("types.json", _context.ProductTypes);
                    _context.SaveChanges();
                }
                if (!HasProducts)
                {
                    await SeedDataFromJsonAsync<Product, int>("products.json", _context.Products);
                    _context.SaveChanges();
                }
                if (!HasDeliveryMethods)
                {
                    await SeedDataFromJsonAsync<DeliveryMethod, int>("delivery.json", _context.DeliveryMethods);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while initializing data.", ex);
            }
        }

        #endregion
        #region Identity Seeding 
        public async Task SeedIdentityDataAsync()
        {
            try
            {
            if(!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        DisplayName = "AdminUser",
                        UserName = "adminUser",
                        Email = "admin@gmail.com",
                        PhoneNumber = "1234567890",

                    };
                    var SuperadminUser = new User
                    {
                        DisplayName = "Ahmed",
                        UserName = "ahmed",
                        Email = "ahmed@gmail.com",
                        PhoneNumber = "1234567899",

                    };
                    await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(SuperadminUser, "Pa$$word");
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperadminUser, "SuperAdmin");
                    
                    }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding identity data.", ex);
            }
        } 
        #endregion
        #region HelperMethods 
        private async Task SeedDataFromJsonAsync<T, TKey>(string fileName, DbSet<T> dbset)
        where T : BaseEntity<TKey>
        {
            var FilePath = Path.Combine(
                Path.GetDirectoryName(typeof(DataInitializer).Assembly.Location)!, 
                "Data",
                "DataSeeding",  
                "JsonFiles",   
                fileName);

            if (!File.Exists(FilePath))
                throw new FileNotFoundException($"JSON file {fileName} is not found at {FilePath}");

            try
            {
                using var DataStream = File.OpenRead(FilePath);
                var Data = await JsonSerializer.DeserializeAsync<List<T>>(DataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (Data is not null && !dbset.Any())
                    await dbset.AddRangeAsync(Data);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while seeding data from {fileName}.", ex);
            }
        }

        #endregion
    }
}

