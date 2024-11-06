namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class CoverLetter : OpenHRCoreBaseEntity
    {
        public required string JobApplicationId { get; set; }
        public required virtual JobApplication JobApplication { get; set; }
        public required string FilePath { get; set; }
        public required string FileType { get; set; }
    }
}
