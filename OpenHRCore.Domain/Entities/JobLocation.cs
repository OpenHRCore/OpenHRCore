namespace OpenHRCore.Domain.Entities
{
    public class JobLocation : BaseEntity
    {
        public string JobLocationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ParentJobLocationId { get; set; }
        public virtual JobLocation  ParentJobLocation { get; set; }
        public ICollection<JobLocation> ChildJobLocations { get;set;}
    }
}
