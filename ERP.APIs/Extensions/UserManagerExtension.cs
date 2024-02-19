using ERP.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ERP.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<Employee> FindUserAsync(this UserManager<Employee> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.SingleOrDefaultAsync(U => U.Email == email);
            return user;
        }
    }
}
