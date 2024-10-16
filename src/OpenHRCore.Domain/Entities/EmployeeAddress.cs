namespace OpenHRCore.Domain.Entities
{
    /// <summary>
    /// Represents a physical address associated with an employee.
    /// </summary>
    public class EmployeeAddress : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee associated with this address.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        public required virtual Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the primary address line, typically the street address or P.O. Box.
        /// </summary>
        public required string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the secondary address line, such as an apartment or suite number.
        /// </summary>
        public string? AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the neighborhood or sub-district.
        /// </summary>
        public string? SubDistrict { get; set; }

        /// <summary>
        /// Gets or sets the district or city district.
        /// </summary>
        public string? District { get; set; }

        /// <summary>
        /// Gets or sets the city or town.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the state, province, or region.
        /// </summary>
        public string? StateOrProvince { get; set; }

        /// <summary>
        /// Gets or sets the postal or ZIP code.
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country code (ISO 3166-1 alpha-2).
        /// </summary>
        public required string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the type of address.
        /// </summary>
        public AddressType AddressType { get; set; }

        /// <summary>
        /// Gets or sets any additional address details or notes.
        /// </summary>
        public string? AddressNotes { get; set; }
    }

}
