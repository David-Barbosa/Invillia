using Invillia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invillia.Infra.Mappings
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("game");
            builder.Ignore(x => x.Notifications);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .HasColumnName("id");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("id_user");

            builder.HasOne(x => x.User);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.HasMany(x => x.Loans)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);

            builder.Property(x => x.Available)
                .HasColumnName("available");

            builder.Property(x => x.Active)
                .HasColumnName("active");

            builder.Property(x => x.Exclude)
                .HasColumnName("exclude");
        }
    }
}
