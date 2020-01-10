using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComProvis.AV.Data.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(e => e.Address).HasMaxLength(255);

            builder.Property(e => e.ContactEmail).HasMaxLength(255);

            builder.Property(e => e.ContactFirstName).HasMaxLength(255);

            builder.Property(e => e.ContactLastName).HasMaxLength(255);

            builder.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(e => e.LastChangeDate).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
