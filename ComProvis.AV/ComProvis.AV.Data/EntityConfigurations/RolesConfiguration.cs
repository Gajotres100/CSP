using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComProvis.AV.Data.EntityConfigurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Description).HasMaxLength(450);

            builder.Property(e => e.ExternalId).HasMaxLength(36);

            builder.Property(e => e.Name).HasMaxLength(250);
        }
    }
}
