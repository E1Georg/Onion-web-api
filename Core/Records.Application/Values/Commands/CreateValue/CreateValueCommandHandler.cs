using MediatR;
using Records.Application.Interfaces;
using Records.Domain;

namespace Records.Application.Values.Commands.CreateValue
{
    public class CreateValueCommandHandler : IRequestHandler<CreateValueCommand, Guid>
    {
        private readonly IValuesDbContext _dbContext;
        public CreateValueCommandHandler(IValuesDbContext dbContext) => _dbContext = dbContext;
        public async Task<Guid> Handle(CreateValueCommand request, CancellationToken cancellationToken)
        {       
            var Temp = new Value
            {
                Id = Guid.NewGuid(),
                dateTime = request.dateTime,
                timeInt = request.timeInt,
                timeFloat = request.timeFloat,
                filename= request.filename
            };

            await _dbContext.Values.AddAsync(Temp, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Temp.Id;
        }
    }
}
