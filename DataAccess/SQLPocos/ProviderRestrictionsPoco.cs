using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderRestrictionsPoco
    {
        public int id { get; set; }
        public int? PatientID { get; set; }
        public int? ProviderID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderRestrictionsDomain MapToDomainModel()
        {
            ProviderRestrictionsDomain domain = new ProviderRestrictionsDomain
            {
                id = id,
                PatientID = PatientID,
                ProviderID = ProviderID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderRestrictionsPoco() { }

        public ProviderRestrictionsPoco(ProviderRestrictionsDomain domain)
        {
            id = domain.id;
            PatientID = domain.PatientID;
            ProviderID = domain.ProviderID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
