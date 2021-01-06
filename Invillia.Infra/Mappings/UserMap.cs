using Invillia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invillia.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.Ignore(x => x.Notifications);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .HasColumnName("id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("username");

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(32)
                .IsFixedLength();

            builder.Property(x => x.Active)
                .HasColumnName("active");
        }
    }
}
