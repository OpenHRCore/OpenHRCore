using OpenHRCore.Domain.EmployeeModule.Enums;

namespace OpenHRCore.Domain.EmployeeModule.Entities
{
    /// <summary>
    /// Represents an identity card associated with an employee in the OpenHR Core system.
    /// This entity encapsulates all relevant information about an employee's identity card,
    /// including its details, status, and associated metadata.
    /// </summary>
    public class EmployeeIdentityCard : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this identity card.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship between the identity card and a specific employee.
        /// It should correspond to the Id of an existing EmployeeInfo entity.
        /// </remarks>
        public required Guid EmployeeId { get; set; }
        public required virtual EmployeeInfo Employee { get; set; }

        /// <summary>
        /// Gets or sets the unique number or identifier of the identity card.
        /// </summary>
        /// <remarks>
        /// This should be the official number as it appears on the identity card.
        /// Consider implementing validation to ensure the format is correct based on the card type.
        /// </remarks>
        public required string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the identity card.
        /// </summary>
        /// <remarks>
        /// This property uses the IdentityCardType enum to ensure consistency and facilitate reporting.
        /// </remarks>
        public IdentityCardType CardType { get; set; }

        /// <summary>
        /// Gets or sets the date when the identity card was issued.
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Gets or sets the expiry date of the identity card, if applicable.
        /// </summary>
        /// <remarks>
        /// This property is nullable as some identity cards may not have an expiry date.
        /// </remarks>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the authority that issued the identity card.
        /// </summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>
        /// Gets or sets the file path where a digital copy or scan of the identity card is stored.
        /// </summary>
        /// <remarks>
        /// This should be the full path where the file is stored, which may be a local file system path,
        /// a URL for cloud storage, or any other relevant storage identifier.
        /// </remarks>
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets or sets the current status of the identity card.
        /// </summary>
        /// <remarks>
        /// This property uses the IdentityCardStatus enum to represent the current state of the card,
        /// such as Active, Expired, or Revoked.
        /// </remarks>
        public IdentityCardStatus Status { get; set; }
    }
}
