using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bisopi___Proyectos.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BisopiRoleAuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private string ViewName;

        public BisopiRoleAuthAttribute(string viewName)
        {
            ViewName = viewName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        
        {
            var dbContext = context.HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;
            var roleManager = context.HttpContext.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            var userManager = context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;

            if (dbContext == null || roleManager == null || userManager == null)
            {
                throw new Exception("FATAL ERROR - COULD NOT INJECT SERVICES");
            }

            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var identityRoles = await roleManager.Roles.ToListAsync();

            var roleIds = identityRoles.Where(x => userRoles.Contains(x.Name)).Select(x => x.Id).ToList();
            var allowedView = await dbContext.AllowedViews.FirstOrDefaultAsync(x => x.Name == ViewName);

            if (allowedView == null)
            {
                throw new Exception("FATAL ERROR - VIEW NOT FOUND");
            }

            var allowedViewForRole = await dbContext.AllowedViewsForRoles.FirstOrDefaultAsync(x => roleIds.Contains(x.RoleId.ToString()) && x.AllowedViewId == allowedView.Id);

            if (allowedViewForRole == null)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
