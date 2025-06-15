namespace OpenHRCore.SharedKernel.Domain
{
    public abstract class OpenHRCoreBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
