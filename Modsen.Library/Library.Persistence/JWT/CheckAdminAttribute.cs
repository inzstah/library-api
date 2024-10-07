using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.Persistence.JWT
{
    public class CheckAdminAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var IsAdminClaim = user.Claims.FirstOrDefault(c => c.Type == "IsAdmin");
                if (IsAdminClaim == null || !bool.TryParse(IsAdminClaim.Value, out bool IsAdmin) || !IsAdmin)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
