﻿namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class Resume : OpenHRCoreBaseEntity
    {
        public required Guid JobApplicationId { get; set; }
        public virtual JobApplication? JobApplication { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public required string FileType { get; set; }
    }
}
