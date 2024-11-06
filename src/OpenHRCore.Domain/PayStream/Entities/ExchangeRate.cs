namespace OpenHRCore.Domain.PayStream.Entities
{
    public class ExchangeRate : OpenHRCoreBaseEntity
    {
        public required string SourceCurrencyId { get; set; }
        public required virtual Currency SourceCurrency { get; set; }
        public required string TargetCurrencyId { get; set; }
        public required virtual Currency TargetCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
