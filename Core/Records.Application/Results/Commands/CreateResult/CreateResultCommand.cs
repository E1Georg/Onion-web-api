using MediatR;

namespace Records.Application.Results.Commands.CreateResult
{
    public class CreateResultCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public long allTime { get; set; }
        public DateTime minValueDatetime { get; set; }
        public double averageTime { get; set; }
        public double averageIndicator { get; set; }
        public double medianaOfIndicator { get; set; }
        public float maxValueOfIndicator { get; set; }
        public float minValueOfIndicator { get; set; }
        public int countOfString { get; set; }
        public string filename { get; set; }
    }
}
