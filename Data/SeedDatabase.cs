using Microsoft.AspNetCore.Identity;

namespace dotnet_store.Data;

public static class SeedDatabase
{
    public static async Task Initialize(IApplicationBuilder app)
    {
        var userManager = app.ApplicationServices
                            .CreateScope()
                            .ServiceProvider
                            .GetRequiredService<UserManager<AppUser>>();

        var roleManager = app.ApplicationServices
                            .CreateScope()
                            .ServiceProvider
                            .GetRequiredService<RoleManager<AppRole>>();

        if (!roleManager.Roles.Any())
        {
            var admin = new AppRole { Name = "Admin" };
            await roleManager.CreateAsync(admin);
        }

        if (!userManager.Users.Any())
        {
            var admin = new AppUser
            {
                FullName = "Admin",
                UserName = "admin@info.com",
                Email = "admin@info.com",
            };

            await userManager.CreateAsync(admin, "admin123");
            await userManager.AddToRoleAsync(admin, "Admin");

            var customer = new AppUser
            {
                FullName = "Customer",
                UserName = "customer@info.com",
                Email = "customer@info.com",
            };
            await userManager.CreateAsync(customer, "customer123");
        }
    }
}