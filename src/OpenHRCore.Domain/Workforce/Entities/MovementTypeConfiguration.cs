using OpenHRCore.Domain.Workforce.Enums;

namespace OpenHRCore.Domain.Workforce.Entities
{
    public class MovementTypeConfiguration : OpenHRCoreBaseEntity
    {
        public MovementType MovementType { get; set; }
        public virtual ICollection<ChangeType> AllowedChanges { get; set; } = new List<ChangeType>(); // Specifies what fields can be changed for each movement type
    }
}
