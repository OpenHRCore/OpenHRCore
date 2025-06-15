namespace OpenHRCore.Recruitment.Domain.Entities
{
    public class JobApplicationHistory : OpenHRCoreBaseEntity
    {
        public required Guid JobApplicationId { get; set; }
        public virtual JobApplication? JobApplication { get; set; }

        public required Guid FromApplicationStageId { get; set; }
        public virtual ApplicationStage? FromApplicationStage { get; set; }
        public required StageStatus FromStageStatus { get; set; }

        public required Guid ToApplicationStageId { get; set; }
        public virtual ApplicationStage? ToApplicationStage { get; set; }
        public required StageStatus ToStageStatus { get; set; }

        public string? ChangedBy { get; set; }
        public DateTime? ChangedAt { get; set; }
        public string? Note { get; set; }

        public string? AIFeedback { get; set; } 
        public decimal? AISource { get; set; } 
        public string? AISummary { get; set; }
        public string? AIAgentName { get; set; } 
        public string? LLMModel { get; set; }
        public string? LLMProvider { get; set; }
        public string? AIPromptSnapshot { get; set; }
        public string? AIResponseSnapshot { get; set; }
    }

}
