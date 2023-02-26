using MediatR;
using Records.Application.Interfaces;
using Records.Domain;

namespace Records.Application.Results.Commands.CreateResult
{
    public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, Guid>
    {
        private readonly IResultsDbContext _dbContext;
        public CreateResultCommandHandler(IResultsDbContext dbContext) => _dbContext = dbContext;

        public async Task<Guid> Handle(CreateResultCommand request, CancellationToken cancellationToken)
        {
            var Temp = new Result
            {
                Id = Guid.NewGuid(),
                allTime = request.allTime,
                minValueDatetime = request.minValueDatetime,
                averageTime = request.averageTime,
                averageIndicator = request.averageIndicator,
                medianaOfIndicator = request.medianaOfIndicator,
                maxValueOfIndicator = request.maxValueOfIndicator,
                minValueOfIndicator = request.minValueOfIndicator,
                countOfString = request.countOfString,
                filename= request.filename
            };

            await _dbContext.Results.AddAsync(Temp, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Temp.Id;
        }

    }
}
