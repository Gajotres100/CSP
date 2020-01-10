using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComProvis.AV.Data.EntityConfigurations
{
    public class ApplicationSubscriptionConfiguration : IEntityTypeConfiguration<ApplicationSubscription>
    {
        public void Configure(EntityTypeBuilder<ApplicationSubscription> builder)
        {
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CompanyId).HasColumnName("CompanyID");

            builder.Property(e => e.ProductName).HasMaxLength(100);

            builder.Property(e => e.ServiceUrl).HasMaxLength(50);

            builder.Property(e => e.SubscriptionId)
                .HasColumnName("SubscriptionID")
                .HasMaxLength(100);

            builder.HasOne(d => d.Company)
                .WithMany(p => p.ApplicationSubscription)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationSubscription_Company");
        }
    }
}
