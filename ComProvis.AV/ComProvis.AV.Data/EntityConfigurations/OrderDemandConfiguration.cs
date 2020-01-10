using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComProvis.AV.Data.EntityConfigurations
{
    public class OrderDemandConfiguration : IEntityTypeConfiguration<OrderDemand>
    {
        public void Configure(EntityTypeBuilder<OrderDemand> builder)
        {
            builder.Property(e => e.CreateDate)
                 .HasColumnType("datetime")
                 .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ExternalCompanyId).HasMaxLength(36);

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(36);
        }
    }
}
