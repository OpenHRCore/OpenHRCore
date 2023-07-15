namespace OpenHRCore.Employee.Domain
{
    public class Organization
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentOrganizationId { get; set; }
        public int SortKey { get; set; }
    }
}
