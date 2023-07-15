namespace OpenHRCore.Employee.Domain
{
    public class EmployeeAttachment
    {
        public string AttachmentId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
