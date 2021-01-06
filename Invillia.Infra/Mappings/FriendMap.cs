using Invillia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invillia.Infra.Mappings
{
    public class FriendMap : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("friend");
            builder.Ignore(x => x.Notifications);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .HasColumnName("id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.Property(x => x.CellPhone)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("cellPhone");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("id_user");

            builder.HasOne(x => x.User);

            builder.HasMany(x => x.Loans)
                .WithOne(x => x.Friend)
                .HasForeignKey(x => x.FriendId);

            builder.Property(x => x.Active)
                .HasColumnName("active");

            builder.Property(x => x.Exclude)
                .HasColumnName("exclude");
        }
    }
}
