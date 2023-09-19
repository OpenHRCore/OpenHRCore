using OpenHRCore.Domain.Common;
using OpenHRCore.Domain.Enums.Employees;

namespace OpenHRCore.Domain.Entities.Employees.Information
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
