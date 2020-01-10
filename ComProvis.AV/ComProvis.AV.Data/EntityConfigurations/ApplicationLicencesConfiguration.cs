using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComProvis.AV.Data.EntityConfigurations
{
    public class ApplicationLicencesConfiguration : IEntityTypeConfiguration<ApplicationLicences>
    {
        public void Configure(EntityTypeBuilder<ApplicationLicences> builder)
        {
            builder.Property(e => e.LicenceExpirationDate).HasColumnType("datetime");

            builder.Property(e => e.LicenceKey)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.LicenceStartDate).HasColumnType("datetime");

            builder.Property(e => e.StartChargeDate).HasColumnType("datetime");

            builder.Property(e => e.Version).HasMaxLength(100);

            builder.HasOne(d => d.Subscription)
                .WithMany(p => p.ApplicationLicences)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationLicences_ApplicationSubscription");
        }
    }
}
