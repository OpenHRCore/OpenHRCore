using OpenHRCore.Domain.Common;

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

        //Profile Image
        public string ProfileImagePath { get; set; }
        public string ProfileImageName { get; set; }
        public string ProfileImageOriginalName { get; set; }
        public string ProfileImageExtension { get; set; }

    }
}
