namespace OpenHRCore.Domain.PayStream.Entities
{
    public class CountryConfiguration : OpenHRCoreBaseEntity
    {
        public required string CountryName { get; set; } // e.g., United States, Thailand
        public required string CountryCode { get; set; } // ISO Country Code e.g., US, TH
        public virtual ICollection<TaxConfiguration> TaxConfigurations { get; set; } = new List<TaxConfiguration>();
        public virtual ICollection<SSCConfiguration> SSCConfigurations { get; set; } = new List<SSCConfiguration>();
    }
}
