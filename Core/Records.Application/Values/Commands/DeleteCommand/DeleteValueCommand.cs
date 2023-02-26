using MediatR;

namespace Records.Application.Values.Commands.DeleteCommand
{
    public class DeleteValueCommand : IRequest
    {
        public string filename { get; set; }
    }
}