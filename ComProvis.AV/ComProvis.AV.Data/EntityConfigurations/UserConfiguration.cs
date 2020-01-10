using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComProvis.AV.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Address).HasMaxLength(255);

            builder.Property(e => e.ContactInfo).HasMaxLength(255);

            builder.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email).HasMaxLength(255);

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(e => e.FirstName).HasMaxLength(255);

            builder.Property(e => e.LastChangeDate).HasColumnType("datetime");

            builder.Property(e => e.LastName).HasMaxLength(255);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
