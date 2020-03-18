using Microsoft.EntityFrameworkCore;

namespace SpamReportsManagementSystem.Store
{
    public class UserEntityConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            var entity = builder.Entity<UserEntity>();

            entity.ToTable("User", "srms")
                .HasKey(x => x.Uid);

            entity.Property(x => x.Uid)
                .ValueGeneratedNever();
        }
    }
}
