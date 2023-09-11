namespace OpenHRCore.Domain.Entities
{
    public class Position : BaseEntity
    {
        public string PositionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get;set;}
    }
}
