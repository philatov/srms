using Microsoft.EntityFrameworkCore;

namespace SpamReportsManagementSystem.Store
{
    public sealed class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ReportEntityConfiguration.Configure(modelBuilder);
            UserEntityConfiguration.Configure(modelBuilder);
            ManagerEntityConfiguration.Configure(modelBuilder);
            LockoutEntityConfiguration.Configure(modelBuilder);
            WarningEntityConfiguration.Configure(modelBuilder);
            MessageEntityConfiguration.Configure(modelBuilder);            
        }
    }
}
