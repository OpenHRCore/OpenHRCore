namespace OpenHRCore.Domain.Entities
{
    public class Race : BaseEntity
    {
        public string RaceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
