using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser
                {
                   
                    Email = "abosamour990@gmail.com",
                    address = new Address
                    {
                        FirstName = "mahmoud",
                        LastName = "hassan",
                        Phone = "01012139683",
                       
                        Country = "Egypt",
                        FacebookProfile = "facebook",
                       


                    }

                };
                await userManager.CreateAsync(User,"mahm01142");
            }
        }
    }
}
