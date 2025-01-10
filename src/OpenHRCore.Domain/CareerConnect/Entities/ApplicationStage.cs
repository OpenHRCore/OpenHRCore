namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class ApplicationStage : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<StageStatus> StageStatuses { get; set; } = new List<StageStatus>();
    }
}
