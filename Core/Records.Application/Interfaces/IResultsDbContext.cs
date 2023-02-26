using Microsoft.EntityFrameworkCore;
using Records.Domain;

namespace Records.Application.Interfaces
{
    public interface IResultsDbContext
    {
        DbSet<Result> Results { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
