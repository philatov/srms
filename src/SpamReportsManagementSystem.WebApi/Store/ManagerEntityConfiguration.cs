using Microsoft.EntityFrameworkCore;

namespace SpamReportsManagementSystem.Store
{
    public class ManagerEntityConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            var entity = builder.Entity<ManagerEntity>();

            entity.ToTable("Manager", "srms")
                .HasKey(x => x.Uid);

            entity.Property(x => x.Uid)
                .ValueGeneratedNever();
        }
    }
}
