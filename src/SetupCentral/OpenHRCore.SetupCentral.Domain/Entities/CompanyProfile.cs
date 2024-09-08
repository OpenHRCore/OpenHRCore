using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.SetupCentral.Domain.Entities
{
    /// <summary>
    /// Represents a company profile entity.
    /// </summary>
    public class CompanyProfile : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the file path of the company profile image.
        /// </summary>
        public string? ProfileImagePath { get; set; }

        /// <summary>
        /// Gets or sets the description of the company.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the email address of the company.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the company.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the address of the company.
        /// </summary>
        public string? Address { get; set; }
    }
}
