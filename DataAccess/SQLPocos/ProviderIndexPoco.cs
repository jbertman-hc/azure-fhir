using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderIndexPoco
    {
        public int ProviderIndexId { get; set; }
        public string ProviderCode { get; set; }
        public string ExternalProviderName { get; set; }
        public string ExternalProviderPassword { get; set; }
        public string Source { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ExternalProviderID { get; set; }
        public string ExternalProviderData { get; set; }

        public ProviderIndexDomain MapToDomainModel()
        {
            ProviderIndexDomain domain = new ProviderIndexDomain
            {
                ProviderIndexId = ProviderIndexId,
                ProviderCode = ProviderCode,
                ExternalProviderName = ExternalProviderName,
                ExternalProviderPassword = ExternalProviderPassword,
                Source = Source,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ExternalProviderID = ExternalProviderID,
                ExternalProviderData = ExternalProviderData
            };

            return domain;
        }

        public ProviderIndexPoco() { }

        public ProviderIndexPoco(ProviderIndexDomain domain)
        {
            ProviderIndexId = domain.ProviderIndexId;
            ProviderCode = domain.ProviderCode;
            ExternalProviderName = domain.ExternalProviderName;
            ExternalProviderPassword = domain.ExternalProviderPassword;
            Source = domain.Source;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ExternalProviderID = domain.ExternalProviderID;
            ExternalProviderData = domain.ExternalProviderData;
        }
    }
}
