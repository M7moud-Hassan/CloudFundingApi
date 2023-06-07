using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CroudFundingApi.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindUserByClaimPrincpleWithAddress(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            var email= user.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(x => x.address).SingleOrDefaultAsync(x => x.Email == email);
        }
        public static async Task<AppUser> FindEmailFromClaimPrincipal(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            return await userManager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }

    }
}
