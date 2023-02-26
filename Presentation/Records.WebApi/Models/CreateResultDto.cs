using Records.Application.Common.Mappings;
using Records.Application.Results.Commands.CreateResult;
using AutoMapper;

namespace Records.WebApi.Models
{
    public class CreateResultDto : IMapWith<CreateResultCommand>
    {       
        public long allTime { get; set; }
        public DateTime minValueDatetime { get; set; }
        public double averageTime { get; set; }
        public double averageIndicator { get; set; }
        public double medianaOfIndicator { get; set; }
        public float maxValueOfIndicator { get; set; }
        public float minValueOfIndicator { get; set; }
        public int countOfString { get; set; }
        public string filename { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateResultDto, CreateResultCommand>()
            .ForMember(resultCommand => resultCommand.allTime,
            opt => opt.MapFrom(resultDto => resultDto.allTime))
            .ForMember(resultCommand => resultCommand.minValueDatetime,
            opt => opt.MapFrom(resultDto => resultDto.minValueDatetime))
            .ForMember(resultCommand => resultCommand.averageTime,
            opt => opt.MapFrom(resultDto => resultDto.averageTime))
             .ForMember(resultCommand => resultCommand.averageIndicator,
            opt => opt.MapFrom(resultDto => resultDto.averageIndicator))
             .ForMember(resultCommand => resultCommand.medianaOfIndicator,
            opt => opt.MapFrom(resultDto => resultDto.medianaOfIndicator))
             .ForMember(resultCommand => resultCommand.maxValueOfIndicator,
            opt => opt.MapFrom(resultDto => resultDto.maxValueOfIndicator))
             .ForMember(resultCommand => resultCommand.minValueOfIndicator,
            opt => opt.MapFrom(resultDto => resultDto.minValueOfIndicator))
             .ForMember(resultCommand => resultCommand.filename,
            opt => opt.MapFrom(resultDto => resultDto.filename))
             .ForMember(resultCommand => resultCommand.countOfString,
            opt => opt.MapFrom(resultDto => resultDto.countOfString));

        }
        public static double Median(List<CreateValueDto> source)
        {
            var sortedList = source.Select(x => x.timeFloat).OrderBy(p => p);

            int count = sortedList.Count();
            int itemIndex = count / 2;
            if (count % 2 == 0)
                return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
            else
                return sortedList.ElementAt(itemIndex);
        }
    }
}
