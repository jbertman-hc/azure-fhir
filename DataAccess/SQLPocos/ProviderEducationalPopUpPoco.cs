using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderEducationalPopUpPoco
    {
        public int ProviderId { get; set; }
        public string UrlInfoId { get; set; }
        public bool HasSeen { get; set; }
        public DateTime DateRecorded { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderEducationalPopUpDomain MapToDomainModel()
        {
            ProviderEducationalPopUpDomain domain = new ProviderEducationalPopUpDomain
            {
                ProviderId = ProviderId,
                UrlInfoId = UrlInfoId,
                HasSeen = HasSeen,
                DateRecorded = DateRecorded,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderEducationalPopUpPoco() { }

        public ProviderEducationalPopUpPoco(ProviderEducationalPopUpDomain domain)
        {
            ProviderId = domain.ProviderId;
            UrlInfoId = domain.UrlInfoId;
            HasSeen = domain.HasSeen;
            DateRecorded = domain.DateRecorded;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
