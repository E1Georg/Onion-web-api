using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Records.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Records.Application.Results.Queries.GetResultList
{
    public class GetResultListQueryHandler : IRequestHandler<GetResultListQuery, ResultListVm>
    {
        private readonly IResultsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetResultListQueryHandler(IResultsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ResultListVm> Handle(GetResultListQuery request, CancellationToken cancellationToken)
        {            
            if (request.filename != null)
            {
                var resultQuery = await _dbContext.Results
               .Where(p => p.filename == request.filename)
               .ProjectTo<ResultLookupDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

                return new ResultListVm { Results = resultQuery };
            }
            else if(request.averageTime != null)
            {
                double minValue = Convert.ToDouble(request.averageTime.Split("-")[0]);
                double maxValue = Convert.ToDouble(request.averageTime.Split("-")[1]);

                var resultQuery = await _dbContext.Results
              .Where(p => (p.averageTime >= minValue) && (p.averageTime <= maxValue))
              .ProjectTo<ResultLookupDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

                return new ResultListVm { Results = resultQuery };
            }
            else if(request.averageIndicator != null)
            {
                double minValue = Convert.ToDouble(request.averageIndicator.Split("-")[0]);
                double maxValue = Convert.ToDouble(request.averageIndicator.Split("-")[1]);

                var resultQuery = await _dbContext.Results
              .Where(p => (p.averageIndicator >= minValue) && (p.averageIndicator <= maxValue))
              .ProjectTo<ResultLookupDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

                return new ResultListVm { Results = resultQuery };
            }
            else if(request.minValueDatetime != null)
            {
                string tmp = request.minValueDatetime.Split("-")[0];
                DateTime minValue = DateTime.ParseExact(tmp, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

                tmp = request.minValueDatetime.Split("-")[1];
                DateTime maxValue = DateTime.ParseExact(tmp, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

                var resultQuery = await _dbContext.Results
              .Where(p => (p.minValueDatetime >= minValue) && (p.minValueDatetime <= maxValue))
              .ProjectTo<ResultLookupDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

                return new ResultListVm { Results = resultQuery };
            }
            else
            {
                var resultQuery = await _dbContext.Results
              .OrderBy(p => p.Id)
              .ProjectTo<ResultLookupDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

                return new ResultListVm { Results = resultQuery };
            }            
        }
    }
}
