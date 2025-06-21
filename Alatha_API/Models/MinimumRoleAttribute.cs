using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Alatha_API.Models
{
    public class MinimumRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly RoleEnum.Role _minimumRole;

        public MinimumRoleAttribute(RoleEnum.Role minimumRole)
        {
            _minimumRole = minimumRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if(roleClaim == null || !Enum.TryParse<RoleEnum.Role>(roleClaim.Value, out var userRole))
            {
                context.Result = new ForbidResult();
                return;
            }

            if (userRole < _minimumRole)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
