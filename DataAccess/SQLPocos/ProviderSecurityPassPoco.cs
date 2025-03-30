using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderSecurityPassPoco
    {
        public int ID { get; set; }
        public int? ProviderID { get; set; }
        public string ProviderPass { get; set; }
        public DateTime? PassStartDate { get; set; }
        public DateTime? PassEndDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderSecurityPassDomain MapToDomainModel()
        {
            ProviderSecurityPassDomain domain = new ProviderSecurityPassDomain
            {
                ID = ID,
                ProviderID = ProviderID,
                ProviderPass = ProviderPass,
                PassStartDate = PassStartDate,
                PassEndDate = PassEndDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderSecurityPassPoco() { }

        public ProviderSecurityPassPoco(ProviderSecurityPassDomain domain)
        {
            ID = domain.ID;
            ProviderID = domain.ProviderID;
            ProviderPass = domain.ProviderPass;
            PassStartDate = domain.PassStartDate;
            PassEndDate = domain.PassEndDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
