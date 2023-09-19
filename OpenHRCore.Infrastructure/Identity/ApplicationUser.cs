using Microsoft.AspNetCore.Identity;
using OpenHRCore.Domain.Entities.Employees.Information;

namespace OpenHRCore.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
