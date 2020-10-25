using AutoMapper;
using F1Teams.Models.DTOs;
using F1Teams.Models.Entities;

namespace F1Teams.BL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamDto, Team>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.FoundationYear, opt => opt.MapFrom(src => src.FoundationYear))
                .ForMember(dest => dest.WonChampionsTitle, opt => opt.MapFrom(src => src.WonChampionsTitle))
                .ForMember(dest => dest.IsEntryFeePayed, opt => opt.MapFrom(src => src.IsEntryFeePayed));

            CreateMap<Team, TeamDto>();
        }
    }
}
