﻿using Bisopi___Proyectos.Models;
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

        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<InvoiceReport> InvoiceReports { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<MilestoneTemp> MilestonesTemps { get; set; }
        public DbSet<Programming> Programmings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCommitment> ProjectCommitments { get; set; }
        public DbSet<ProjectStatus> ProjectsStatus { get; set; }
        public DbSet<ProjectTracking> ProjectTrackings { get; set; }
        public DbSet<ProjectType> ProjectsTypes { get; set; }
        public DbSet<ProposalStatus> ProposalsStatus { get; set; }
        public DbSet<ProjectTaskStatus> ProjectTaskStatus { get; set; }
        public DbSet<QuoteStatus> QuotesStatus { get; set; }
        public DbSet<RepresentativeMarketRate> RepresentativeMarketRates { get; set; }
        public DbSet<ResourcePlanning> ResourcesPlannings { get; set; }
        public DbSet<ResourcePlanningReal> ResourcePlanningReals { get; set; }
        public DbSet<ResourcePlanningTemp> ResourcesPlanningsTemps { get; set; }
        public DbSet<RetentionPercentage> RetentionPercentages { get; set; }
        public DbSet<Seniority> Seniorities { get; set; }
        public DbSet<Study> Studies { get; set; }
        public DbSet<SupportStatus> SupportsStatus { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<UserStudy> UserStudies { get; set; }
        public DbSet<UserToolLevel> UserToolLevels { get; set; }
        public DbSet<ProjectTask> ProjectTask{ get; set; }
        public DbSet<TaskGroup> TaskGroup{ get; set; }
        public DbSet<AllowedView> AllowedViews { get; set; }
        public DbSet<AllowedViewForRole> AllowedViewsForRoles { get; set; }
        public DbSet<AllowedViewForGroup> AllowedViewsForGroups { get; set; }
        public DbSet<ProjectTaskRegistry> TaskRegistry { get; set; }
    }
}
