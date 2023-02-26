using Records.Application.Common.Mappings;
using Records.Domain;
using AutoMapper;

namespace Records.Application.Values.Queries.GetValueList
{
    public class ValueLookupDto : IMapWith<Value>
    {
        public Guid Id { get; set; }
        public DateTime dateTime { get; set; }
        public int timeInt { get; set; }
        public float timeFloat { get; set; }
        public string filename { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Value, ValueLookupDto>()
            .ForMember(valueDto => valueDto.Id,
            opt => opt.MapFrom(value => value.Id))
            .ForMember(valueDto => valueDto.dateTime,
            opt => opt.MapFrom(value => value.dateTime))
            .ForMember(valueDto => valueDto.timeInt,
            opt => opt.MapFrom(value => value.timeInt))
            .ForMember(valueDto => valueDto.timeFloat,
            opt => opt.MapFrom(value => value.timeFloat))
             .ForMember(valueDto => valueDto.filename,
            opt => opt.MapFrom(value => value.filename));
        }
    }
}
