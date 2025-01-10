namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class StageStatus : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Guid ApplicationStageId { get; set; }
        public virtual ApplicationStage? Stage { get; set; }
        public bool IsFinalStatus { get; set; } = false;
    }
}
