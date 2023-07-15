namespace OpenHRCore.Employee.Domain
{
    public class Currency
    {
        public string CurrencyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsHomeCurrency { get; set; }
    }
}
