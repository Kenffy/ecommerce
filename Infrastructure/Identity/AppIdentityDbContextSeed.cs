using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Kenffy",
                    Email = "kenffy@gmail.com",
                    UserName = "kenffy@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Kenffy",
                        LastName = "Dave",
                        Street = "Kenffy Street 19",
                        City = "Stuttgart",
                        State = "Germany",
                        ZipCode = "71332"
                    }
                };

                await userManager.CreateAsync(user, "Kenffy420*");
            }
        }
    }
}