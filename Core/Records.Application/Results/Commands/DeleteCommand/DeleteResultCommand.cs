using MediatR;

namespace Records.Application.Results.Commands.DeleteCommand
{
    public class DeleteResultCommand : IRequest
    {
        public string filename { get; set; }
    }
}
