using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents an address associated with an employee.
    /// </summary>
    public class EmployeeAddress : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this address.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first line of the address.
        /// </summary>
        public required string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the address (optional).
        /// </summary>
        public string? AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the sub-district of the address (optional).
        /// </summary>
        public string? SubDistrict { get; set; }

        /// <summary>
        /// Gets or sets the district of the address (optional).
        /// </summary>
        public string? District { get; set; }

        /// <summary>
        /// Gets or sets the province of the address (optional).
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address (optional).
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the type of address (e.g., Home, Work) (optional).
        /// </summary>
        public AddressType? AddressType { get; set; }
    }

    /// <summary>
    /// Enum for address types (e.g., Home, Work).
    /// </summary>
    public enum AddressType
    {
        Home,
        Work
    }
}
