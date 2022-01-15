using AutoMapper;
using Net6.Api.DataContext;
using Net6.Api.DataTransport;

namespace Net6.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
        }
    }
}
