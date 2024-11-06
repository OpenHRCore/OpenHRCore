namespace OpenHRCore.Domain.PayStream.Entities
{
    public class SSCConfiguration : OpenHRCoreBaseEntity
    {
        public required string CountryConfigurationId { get; set; } // New Foreign Key
        public required virtual CountryConfiguration CountryConfiguration { get; set; } // Navigation Property
        public decimal SSCRate { get; set; } // Social Security Contribution rate (e.g., 0.04 for 4%)
        public DateTime EffectiveDate { get; set; }
    }
}
