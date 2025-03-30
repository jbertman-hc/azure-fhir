using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PreferencesPoco
    {
        public int UniqueTableID { get; set; }
        public string ProviderName { get; set; }
        public string PreferenceName { get; set; }
        public string PreferenceLocation { get; set; }
        public string PreferenceValue { get; set; }
        public bool PracticeWide { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PreferencesDomain MapToDomainModel()
        {
            PreferencesDomain domain = new PreferencesDomain
            {
                UniqueTableID = UniqueTableID,
                ProviderName = ProviderName,
                PreferenceName = PreferenceName,
                PreferenceLocation = PreferenceLocation,
                PreferenceValue = PreferenceValue,
                PracticeWide = PracticeWide,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PreferencesPoco() { }

        public PreferencesPoco(PreferencesDomain domain)
        {
            UniqueTableID = domain.UniqueTableID;
            ProviderName = domain.ProviderName;
            PreferenceName = domain.PreferenceName;
            PreferenceLocation = domain.PreferenceLocation;
            PreferenceValue = domain.PreferenceValue;
            PracticeWide = domain.PracticeWide;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
