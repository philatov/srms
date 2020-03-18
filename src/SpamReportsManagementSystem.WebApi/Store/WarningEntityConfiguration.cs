using Microsoft.EntityFrameworkCore;

namespace SpamReportsManagementSystem.Store
{
    public class WarningEntityConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            var entity = builder.Entity<WarningEntity>();

            entity.ToTable("Warning", "srms")
                .HasKey(x => x.Uid);

            entity.Property(x => x.Uid)
                .ValueGeneratedNever();

            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Manager).WithMany().HasForeignKey(x => x.ManagerId);
            entity.HasOne(x => x.Report).WithMany().HasForeignKey(x => x.ReportId);
        }
    }
}
