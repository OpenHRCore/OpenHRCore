namespace OpenHRCore.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string CurrencyId { get; set; }  
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsHomeCurrency { get; set; }
    }
}
