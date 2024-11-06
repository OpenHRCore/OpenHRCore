namespace OpenHRCore.Domain.Workforce.Entities
{
    public class JobPosition : OpenHRCoreBaseEntity
    {
        public required string Title { get; set; }
        public required string DepartmentId { get; set; }
        public required virtual Department Department { get; set; }
    }
}
