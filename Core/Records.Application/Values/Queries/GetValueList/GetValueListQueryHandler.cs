using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Records.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Records.Application.Values.Queries.GetValueList
{
    public class GetValueListQueryHandler : IRequestHandler<GetValueListQuery, ValueListVm>
    {
        private readonly IValuesDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetValueListQueryHandler(IValuesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ValueListVm> Handle(GetValueListQuery request, CancellationToken cancellationToken)
        {
            var valueQuery = await _dbContext.Values
                .Where(p => p.filename == request.filename)
                .ProjectTo<ValueLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ValueListVm { Values = valueQuery };
        }
    }
}
