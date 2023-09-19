using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Setup
{
    public class Township : BaseEntity
    {
        public string TownshipId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string CityId { get; set; }
        public virtual City City { get; set; }
    }
}
