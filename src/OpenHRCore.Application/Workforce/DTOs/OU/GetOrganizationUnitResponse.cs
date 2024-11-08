namespace OpenHRCore.Application.Workforce.DTOs.OU
{
    public class GetOrganizationUnitResponse
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? ParentOrganizationUnitId { get; set; }
        public string? ParentOrganizationCode { get; set; }
        public string? ParentOrganizationName { get; set; }
        public string? ParentOrganizationDescription { get; set; }
        public ICollection<GetOrganizationUnitResponse> SubOrganizationUnits { get; set; } = new List<GetOrganizationUnitResponse>();
        public required int SortOrder { get; set; }
    }
}
