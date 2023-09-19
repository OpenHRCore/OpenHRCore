using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Information
{
    public class EmployeeAttachment : BaseEntity
    {
        public string EmployeeAttachmentId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FileExtension { get; set; }
        public string Description { get; set; }
    }
}
