using MediatR;

namespace Records.Application.Results.Queries.GetResultList
{
    public class GetResultListQuery : IRequest<ResultListVm>
    {      
        public string? minValueDatetime { get; set; }
        public string? averageTime { get; set; }
        public string? averageIndicator { get; set; }
        public string? filename { get; set; }
    }
}
