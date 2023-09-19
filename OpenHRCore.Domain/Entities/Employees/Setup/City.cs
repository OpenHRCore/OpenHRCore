using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Setup
{
    public class City : BaseEntity
    {
        public string CityId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Township> Townships { get; set; }
    }
}
