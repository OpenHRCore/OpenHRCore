using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHRCore.Employee.Domain
{
    public class EmployeeFamily
    {
        public string FamilyMemberId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }  
        public string Name { get; set; }
        public string Relationship { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
    }
}
