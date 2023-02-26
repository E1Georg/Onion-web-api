using MediatR;

namespace Records.Application.Values.Queries.GetValueList
{
    public class GetValueListQuery : IRequest<ValueListVm>
    {
        public string filename { get; set; }
    }
}
