using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bisopi___Proyectos.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<List<string>> GetGroupsAsync(this UserManager<ApplicationUser> userManager, DataContext context, ApplicationUser user)
        {
            var userGroupsIds = await context.UserGroups.Where(x => x.UserId == user.Id).Select(x => x.GroupId).ToListAsync();

            var groups = await context.Groups.Where(x => userGroupsIds.Contains(x.Id)).Select(x => x.Name).ToListAsync();

            return groups;
        }

        public static async Task<bool> IsInGroupAsync(this UserManager<ApplicationUser> userManager, DataContext context, ApplicationUser user, string groupName)
        {
            var group = await context.Groups.FirstOrDefaultAsync(x => x.Name == groupName);

            if (group == null)
            {
                return false;
            }

            var userGroup = await context.UserGroups.FirstOrDefaultAsync(x => x.UserId == user.Id && x.GroupId == group.Id);

            if (userGroup == null)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> OverrideGroups(this UserManager<ApplicationUser> userManager, DataContext context, ApplicationUser user, IEnumerable<string> groupNames)
        {
            try
            {
                await context.UserGroups.Where(x => x.UserId == user.Id).ExecuteDeleteAsync();

                var groups = await context.Groups.Where(x => groupNames.Contains(x.Name)).ToListAsync();

                await context.UserGroups.AddRangeAsync(groups.Select(x => new UserGroup()
                {
                    UserId = user.Id,
                    GroupId = x.Id,
                }));

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: (UserManagerExtension | OverrideGroups) - " + ex.Message);
                return false;
            }
        }
    }
}
