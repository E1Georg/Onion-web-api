using Records.Application.Common.Mappings;
using Records.Application.Values.Commands.CreateValue;
using AutoMapper;
using CsvHelper.Configuration.Attributes;

namespace Records.WebApi.Models
{
    public class CreateValueDto : IMapWith<CreateValueCommand>
    {
        [Index(0)]
        [Format("yyyy-MM-dd_HH-mm-ss")]
        public DateTime dateTime { get; set; }
        [Index(1)]
        public int timeInt { get; set; }
        [Index(2)]
        public float timeFloat { get; set; }
        public string? filename { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateValueDto, CreateValueCommand>()
            .ForMember(valueCommand => valueCommand.dateTime,
            opt => opt.MapFrom(valueDto => valueDto.dateTime))
            .ForMember(valueCommand => valueCommand.timeInt,
            opt => opt.MapFrom(valueDto => valueDto.timeInt))
            .ForMember(valueCommand => valueCommand.timeFloat,
            opt => opt.MapFrom(valueDto => valueDto.timeFloat))
            .ForMember(valueCommand => valueCommand.filename,
            opt => opt.MapFrom(valueDto => valueDto.filename));
        }        
    }
}
