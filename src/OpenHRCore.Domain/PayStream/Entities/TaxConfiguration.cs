namespace OpenHRCore.Domain.PayStream.Entities
{
    public class TaxConfiguration : OpenHRCoreBaseEntity
    {
        public required string CountryConfigurationId { get; set; } // New Foreign Key
        public required virtual CountryConfiguration CountryConfiguration { get; set; } // Navigation Property
        public decimal TaxRate { get; set; } // Tax rate (e.g., 0.06 for 6%)
        public DateTime EffectiveDate { get; set; }
    }
}
