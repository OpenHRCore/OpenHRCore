using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Information
{
    public class EmployeeContactAddress : BaseEntity
    {
        public string EmployeeContactAddressId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string CityId { get; set; }
        public virtual City City { get; set; }
        public string TownshipId { get; set; }
        public virtual Township Township { get; set; }
        public string Address { get; set; }
        public bool IsCurrentAddress { get; set; }
    }
}
