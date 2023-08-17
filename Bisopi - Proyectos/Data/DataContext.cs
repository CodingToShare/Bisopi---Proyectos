using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ModelsTemps;
using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<MilestoneTemp> MilestonesTemps { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCommitment> ProjectCommitments { get; set; }
        public DbSet<ProjectStatus> ProjectsStatus { get; set; }
        public DbSet<ProjectType> ProjectsTypes { get; set; }
        public DbSet<ProposalStatus> ProposalsStatus { get; set; }
        public DbSet<ProjectTaskStatus> ProjectTaskStatus { get; set; }
        public DbSet<QuoteStatus> QuotesStatus { get; set; }
        public DbSet<SupportStatus> SupportsStatus { get; set; }
        public DbSet<Position> Positions{ get; set; }
        public DbSet<ProjectTask> ProjectTask{ get; set; }
        public DbSet<TaskGroup> TaskGroup{ get; set; }

        public DbSet<AllowedView> AllowedViews { get; set; }
        public DbSet<AllowedViewForRole> AllowedViewsForRoles { get; set; }
        public DbSet<AllowedViewForGroup> AllowedViewsForGroups { get; set; }
    }
}
