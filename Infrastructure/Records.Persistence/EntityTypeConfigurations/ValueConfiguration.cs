using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Records.Domain;

namespace Records.Persistence.EntityTypeConfigurations
{
    public class ValueConfiguration : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            builder.HasKey(value => value.Id);
            builder.HasIndex(value => value.Id).IsUnique();
        }

    }
}
