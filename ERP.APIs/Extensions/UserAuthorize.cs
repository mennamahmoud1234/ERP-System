using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.APIs.Extensions
{
    public class UserAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new{ StatusCode = 401 , Message = "Unauthorized access"})
                {
                    StatusCode = 401
                };
            }
        }
    }
}
