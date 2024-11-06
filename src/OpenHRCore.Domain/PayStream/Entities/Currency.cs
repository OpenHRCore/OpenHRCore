namespace OpenHRCore.Domain.PayStream.Entities
{
    public class Currency : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; } // e.g., USD, THB
        public required string Name { get; set; }
    }
}
