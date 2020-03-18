using Microsoft.EntityFrameworkCore;

namespace SpamReportsManagementSystem.Store
{
    public class ReportEntityConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            var entity = builder.Entity<ReportEntity>();

            entity.ToTable("Report", "srms")
                .HasKey(x => x.Uid);

            entity.Property(x => x.Uid)
                .ValueGeneratedNever();

            entity.HasOne(x => x.From).WithMany().HasForeignKey(x => x.FromId);
            entity.HasOne(x => x.About).WithMany().HasForeignKey(x => x.AboutId);
            entity.HasOne(x => x.Manager).WithMany().HasForeignKey(x => x.ManagerId);
        }
    }
}
