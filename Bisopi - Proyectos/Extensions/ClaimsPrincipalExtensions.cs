using Bisopi___Proyectos.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bisopi___Proyectos.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public async static Task<bool> IsInGroupAsync(this ClaimsPrincipal claimsPrincipal, string groupName)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var cs = config.GetValue<string>("ConnectionStrings:DefaultConnection");

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(cs)
                .Options;

            var context = new DataContext(options);

            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            var group = await context.Groups.FirstOrDefaultAsync(x => x.Name == groupName);

            if (group == null)
            {
                return false;
            }

            var userGroup = await context.UserGroups.FirstOrDefaultAsync(x => x.UserId == userId && x.GroupId == group.Id);

            if (userGroup == null)
            {
                return false;
            }

            return true;
        }

        public async static Task<bool> IsViewAvaliableForRole(this ClaimsPrincipal claimsPrincipal, string viewName)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var cs = config.GetValue<string>("ConnectionStrings:DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(cs)
                .Options;

            var optionsDataContext = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(cs)
                .Options;

            var context = new ApplicationDbContext(options);
            var dataContext = new DataContext(optionsDataContext);

            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var roleIds = await context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();

            var allowedView = await dataContext.AllowedViews.FirstOrDefaultAsync(x => x.Name == viewName) ?? throw new Exception("FATAL ERROR - VIEW NOT FOUND");
            var allowedViewForRole = await dataContext.AllowedViewsForRoles.FirstOrDefaultAsync(x => x.AllowedViewId == allowedView.Id && roleIds.Contains(x.RoleId.ToString()));

            if (allowedViewForRole == null)
            {
                return false;
            }

            return true;
        }

        public async static Task<bool> IsViewAvaliableForGroup(this ClaimsPrincipal claimsPrincipal, string viewName)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var cs = config.GetValue<string>("ConnectionStrings:DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(cs)
                .Options;

            var optionsDataContext = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(cs)
                .Options;

            var context = new ApplicationDbContext(options);
            var dataContext = new DataContext(optionsDataContext);

            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var groupIds = await dataContext.UserGroups.Where(x => x.UserId == userId).Select(x => x.GroupId).ToListAsync();

            var allowedView = await dataContext.AllowedViews.FirstOrDefaultAsync(x => x.Name == viewName) ?? throw new Exception("FATAL ERROR - VIEW NOT FOUND");
            var allowedViewForGroup = await dataContext.AllowedViewsForGroups.FirstOrDefaultAsync(x => x.AllowedViewId == allowedView.Id && groupIds.Contains(x.GroupId));

            if (allowedViewForGroup == null)
            {
                return false;
            }

            return true;
        }
    }
}
