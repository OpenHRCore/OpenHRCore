using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Admin
{
    public class ApplicationSetting : BaseEntity
    {
        public string ApplicationSettingId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
