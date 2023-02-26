using MediatR;
using Records.Application.Interfaces;
using Records.Application.Common.Exceptions;
using Records.Domain;

namespace Records.Application.Values.Commands.DeleteCommand
{
    public class DeleteValueCommandHandler : IRequestHandler<DeleteValueCommand>
    {
        private readonly IValuesDbContext _dbContext;
        public DeleteValueCommandHandler(IValuesDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(DeleteValueCommand request, CancellationToken cancellationToken)
        {
            var records = _dbContext.Values.Where(t => t.filename == request.filename).Select(o => o).ToList();

            foreach (var item in records)
            {
                var query = await _dbContext.Values
               .FindAsync(new object[] { item.Id }, cancellationToken);

                if (query == null || query.Id != item.Id)
                {
                    throw new NotFoundException(nameof(Result), request.filename);
                }

                _dbContext.Values.Remove(query);
            }
           
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}