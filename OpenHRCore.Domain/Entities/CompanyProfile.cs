namespace OpenHRCore.Domain.Entities
{
    public class CompanyProfile : BaseEntity
    {
        public string CompanyId { get; set; }   
        public string Code { get; set; }
        public string CompanyName { get; set;}
        public string CompanyDescription { get; set;}      
        public string CompanyEmail { get; set;}
        public string CompanyPhoneNumber { get;set;}

    }
}
