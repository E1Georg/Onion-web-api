using Microsoft.EntityFrameworkCore;
using Records.Domain;

namespace Records.Application.Interfaces
{
    public interface IValuesDbContext
    {
        DbSet<Value> Values { get; set; }       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
