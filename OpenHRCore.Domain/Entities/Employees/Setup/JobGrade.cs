using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Setup
{
    public class JobGrade : BaseEntity
    {
        public string JobGradeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
