using OpenHRCore.Domain.Enums;

namespace OpenHRCore.Domain.Entities
{
    public class EmployeeIdentityCard : BaseEntity
    {
        public string EmployeeIdentityCardId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public IdentityCardType IdentityCardType { get; set; }
        public string CardNumber { get; set; }
        public bool IsUseDefault { get; set; }
    }
}
