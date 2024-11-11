using AutoMapper;
using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Application.Workforce.DTOs.OUDtos
{
    public class OrganizationUnitMappingProfile : Profile
    {
        public OrganizationUnitMappingProfile()
        {
            // Request to Entity
            CreateMap<CreateOrganizationUnitRequest, OrganizationUnit>();
            CreateMap<UpdateOrganizationUnitRequest, OrganizationUnit>();


            // Entity to Response
            CreateMap<OrganizationUnit, GetOrganizationUnitsWithHierarchyResponse>()
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

            CreateMap<OrganizationUnit, GetOrganizationUnitResponse>()
                    .ForMember(dest => dest.ParentOrganizationUnitId,
                               opt => opt.MapFrom(src => src.ParentOrganizationUnitId))
                    .ForMember(dest => dest.ParentOrganizationCode,
                               opt => opt.MapFrom(src => src.ParentOrganizationUnit != null ? src.ParentOrganizationUnit.Code : null))
                    .ForMember(dest => dest.ParentOrganizationName,
                               opt => opt.MapFrom(src => src.ParentOrganizationUnit != null ? src.ParentOrganizationUnit.Name : null))
                    .ForMember(dest => dest.ParentOrganizationDescription,
                               opt => opt.MapFrom(src => src.ParentOrganizationUnit != null ? src.ParentOrganizationUnit.Description : null));

        }
    }
}
