using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListMessagedProviderPoco
    {
        public int PatientId { get; set; }
        public int ListMessagedProviderId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListMessagedProviderDomain MapToDomainModel()
        {
            ListMessagedProviderDomain domain = new ListMessagedProviderDomain
            {
                PatientId = PatientId,
                ListMessagedProviderId = ListMessagedProviderId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListMessagedProviderPoco() { }

        public ListMessagedProviderPoco(ListMessagedProviderDomain domain)
        {
            PatientId = domain.PatientId;
            ListMessagedProviderId = domain.ListMessagedProviderId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
