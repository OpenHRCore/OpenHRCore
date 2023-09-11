namespace OpenHRCore.Domain.Entities
{
    public class EmployeeContactAddress : BaseEntity
    {
        public string EmployeeContactAddressId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsCurrentAddress { get; set; }  
    }
}
