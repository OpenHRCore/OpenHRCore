using AutoMapper;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Application.Workforce.DTOs;

namespace OpenHRCore.Application.Workforce.DTOs.OU
{
    public class OrganizationUnitMappingProfile : Profile
    {
        public OrganizationUnitMappingProfile()
        {
            // Map from CreateOrganizationUnitRequest to OrganizationUnit
            CreateMap<CreateOrganizationUnitRequest, OrganizationUnit>();

            // Map from OrganizationUnit to GetOrganizationUnitResponse, including nested properties
            CreateMap<OrganizationUnit, GetOrganizationUnitResponse>()
                .ForMember(dest => dest.ParentOrganizationUnitId,
                           opt => opt.MapFrom(src => src.ParentOrganizationUnitId))
                .ForMember(dest => dest.SubOrganizationUnits,
                           opt => opt.MapFrom(src => src.SubOrganizationUnits))
                .ForMember(dest => dest.ParentOrganizationCode,
                           opt => opt.MapFrom(src => src.ParentOrganizationUnit != null ? src.ParentOrganizationUnit.Code : null))
                .ForMember(dest => dest.ParentOrganizationName,
                           opt => opt.MapFrom(src => src.ParentOrganizationUnit != null ? src.ParentOrganizationUnit.Name : null))
                .ForMember(dest => dest.ParentOrganizationDescription,
                           opt => opt.MapFrom(src => src.ParentOrganizationUnit != null ? src.ParentOrganizationUnit.Description : null));

        }
    }
}
