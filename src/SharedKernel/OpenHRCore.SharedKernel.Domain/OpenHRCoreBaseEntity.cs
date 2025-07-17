namespace OpenHRCore.SharedKernel.Domain
{
    public abstract class OpenHRCoreBaseEntity
    {
        protected OpenHRCoreBaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public string? CreatedBy { get; protected set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public string? UpdatedBy { get; protected set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsActive { get; protected set; } = true;

        public virtual void MarkAsDeleted(string? deletedBy = null)
        {
            if (IsDeleted) return;
            
            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
            DeletedBy = deletedBy;
            MarkUpdated(deletedBy);
        }

        public virtual void MarkUpdated(string? updatedBy = null)
        {
            UpdatedAt = DateTimeOffset.UtcNow;
            UpdatedBy = updatedBy;
        }
    }
}
