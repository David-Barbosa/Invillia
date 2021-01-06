using Invillia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invillia.Infra.Mappings
{
    public class LoanMap : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("loan");
            builder.Ignore(x => x.Notifications);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .HasColumnName("id");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("id_user");

            builder.HasOne(x => x.User);

            builder.Property(x => x.GameId)
                .IsRequired()
                .HasColumnName("id_game");

            builder.HasOne(x => x.Game);

            builder.Property(x => x.FriendId)
                .IsRequired()
                .HasColumnName("id_friend");

            builder.HasOne(x => x.Friend)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.FriendId);


            builder.Property(x => x.LoanDate)
                .IsRequired()
                .HasColumnName("loan_date");

            builder.Property(x => x.ReturnDate)
                .HasColumnName("return_date");
        }
    }
}
