using MediatR;

namespace Records.Application.Values.Commands.CreateValue
{
    public class CreateValueCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DateTime dateTime { get; set; }
        public int timeInt { get; set; }
        public float timeFloat { get; set; }
        public string filename { get; set; }
    }
}
