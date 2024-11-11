namespace OpenHRCore.Application.Workforce.DTOs.OUDtos
{
    public class GetOrganizationUnitsWithHierarchyResponse
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? ParentOrganizationUnitId { get; set; }
        public string? ParentOrganizationCode { get; set; }
        public string? ParentOrganizationName { get; set; }
        public string? ParentOrganizationDescription { get; set; }
        public ICollection<GetOrganizationUnitsWithHierarchyResponse> SubOrganizationUnits { get; set; } = new List<GetOrganizationUnitsWithHierarchyResponse>();
        public required int SortOrder { get; set; }
    }
}
