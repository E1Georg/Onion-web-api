using MediatR;
using Records.Application.Interfaces;
using Records.Application.Common.Exceptions;
using Records.Domain;

namespace Records.Application.Results.Commands.DeleteCommand
{
    public class DeleteResultCommandHandler : IRequestHandler<DeleteResultCommand>
    {
        private readonly IResultsDbContext _dbContext;
        public DeleteResultCommandHandler(IResultsDbContext dbContext) => _dbContext = dbContext;  
        public async Task<Unit> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            var records = _dbContext.Results.Where(t => t.filename == request.filename).Select(o => o).ToList();

            foreach (var item in records)
            {
                var query = await _dbContext.Results
               .FindAsync(new object[] { item.Id }, cancellationToken);

                if (query == null || query.Id != item.Id)
                {
                    throw new NotFoundException(nameof(Result), request.filename);
                }

                _dbContext.Results.Remove(query);
            } 

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value; 
        }
    }
}
