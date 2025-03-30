using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderBillingMappingPoco
    {
        public int UniqueTableId { get; set; }
        public int ProviderId { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderBillingMappingDomain MapToDomainModel()
        {
            ProviderBillingMappingDomain domain = new ProviderBillingMappingDomain
            {
                UniqueTableId = UniqueTableId,
                ProviderId = ProviderId,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderBillingMappingPoco() { }

        public ProviderBillingMappingPoco(ProviderBillingMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            ProviderId = domain.ProviderId;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
