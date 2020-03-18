using Microsoft.EntityFrameworkCore;

namespace SpamReportsManagementSystem.Store
{
    public class MessageEntityConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            var entity = builder.Entity<MessageEntity>();

            entity.ToTable("Message", "srms")
                .HasKey(x => x.Uid);

            entity.Property(x => x.Uid)
                .ValueGeneratedNever();

            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
