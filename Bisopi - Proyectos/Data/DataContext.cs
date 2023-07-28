using Bisopi___Proyectos.Models;
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
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectsStatus { get; set; }
        public DbSet<ProjectType> ProjectsTypes { get; set; }
        public DbSet<SupportStatus> SupportsStatus { get; set; }
    }
}
