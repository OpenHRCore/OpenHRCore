using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    public class EmployeeIdentityCard : OpenHRCoreBaseEntity
    {
        public required Guid EmployeeId { get; set; }
        public required string CardNumber { get; set; }
        public IdentityCardType CardType { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? IssuingAuthority { get; set; } // Authority that issued the ID
        public string? FilePath { get; set; }
        public IdentityCardStatus Status { get; set; }
        public virtual Employee? Employee { get; set; }
    }

    public enum IdentityCardType
    {
        NationalID,
        Passport,
        DriverLicense,
        WorkPermit,
        CompanyID
    }

    public enum IdentityCardStatus
    {
        Active,
        Expired,
        Revoked
    }

}
