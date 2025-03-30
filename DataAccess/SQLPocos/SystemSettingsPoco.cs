using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SystemSettingsPoco
    {
        public int SSID { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string SettingCategory { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SystemSettingsDomain MapToDomainModel()
        {
            SystemSettingsDomain domain = new SystemSettingsDomain
            {
                SSID = SSID,
                SettingName = SettingName,
                SettingValue = SettingValue,
                SettingCategory = SettingCategory,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SystemSettingsPoco() { }

        public SystemSettingsPoco(SystemSettingsDomain domain)
        {
            SSID = domain.SSID;
            SettingName = domain.SettingName;
            SettingValue = domain.SettingValue;
            SettingCategory = domain.SettingCategory;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
