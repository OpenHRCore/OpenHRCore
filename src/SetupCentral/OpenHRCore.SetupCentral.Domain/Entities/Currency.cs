using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.SetupCentral.Domain.Entities
{
    /// <summary>
    /// Represents a currency entity.
    /// </summary>
    public class Currency : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the code of the currency (e.g., USD, EUR, GBP).
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the symbol of the currency.
        /// </summary>
        public required string Symbol { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the default currency.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate relative to the default currency.
        /// </summary>
        public decimal ExchangeRate { get; set; }
    }
}
