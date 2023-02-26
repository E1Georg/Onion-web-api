using Records.Application.Common.Mappings;
using Records.Domain;
using AutoMapper;

namespace Records.Application.Results.Queries.GetResultList
{
    public class ResultLookupDto : IMapWith<Result>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Result, ResultLookupDto>()
            .ForMember(resultDto => resultDto.Id,
            opt => opt.MapFrom(result => result.Id))
            .ForMember(resultDto => resultDto.allTime,
            opt => opt.MapFrom(result => result.allTime))
            .ForMember(resultDto => resultDto.minValueDatetime,
            opt => opt.MapFrom(result => result.minValueDatetime))
            .ForMember(resultDto => resultDto.averageTime,
            opt => opt.MapFrom(result => result.averageTime))
            .ForMember(resultDto => resultDto.averageIndicator,
            opt => opt.MapFrom(result => result.averageIndicator))
            .ForMember(resultDto => resultDto.medianaOfIndicator,
            opt => opt.MapFrom(result => result.medianaOfIndicator))
            .ForMember(resultDto => resultDto.maxValueOfIndicator,
            opt => opt.MapFrom(result => result.maxValueOfIndicator))
            .ForMember(resultDto => resultDto.minValueOfIndicator,
            opt => opt.MapFrom(result => result.minValueOfIndicator))
             .ForMember(resultDto => resultDto.filename,
            opt => opt.MapFrom(result => result.filename))
            .ForMember(resultDto => resultDto.countOfString,
            opt => opt.MapFrom(result => result.countOfString));
        }
    }
}
