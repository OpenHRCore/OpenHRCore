namespace OpenHRCore.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByName { get; set; }
        public string LastModifiedById { get; set; }
    }
}
