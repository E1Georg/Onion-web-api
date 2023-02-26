using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Records.Domain;

namespace Records.Persistence.EntityTypeConfigurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(result => result.Id);
            builder.HasIndex(result => result.Id).IsUnique();
        }

    }
}
