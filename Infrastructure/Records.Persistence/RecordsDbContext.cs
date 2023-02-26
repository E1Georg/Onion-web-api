using Microsoft.EntityFrameworkCore;
using Records.Application.Interfaces;
using Records.Domain;
using Records.Persistence.EntityTypeConfigurations;

namespace Records.Persistence
{
    public class RecordsDbContext : DbContext, IValuesDbContext, IResultsDbContext
    {
        public DbSet<Value> Values { get; set; }
        public DbSet<Result> Results { get; set; }
        public RecordsDbContext(DbContextOptions<RecordsDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ValueConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
