using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ReferralBillingMappingPoco
    {
        public int UniqueTableId { get; set; }
        public int ReferralId { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ReferralBillingMappingDomain MapToDomainModel()
        {
            ReferralBillingMappingDomain domain = new ReferralBillingMappingDomain
            {
                UniqueTableId = UniqueTableId,
                ReferralId = ReferralId,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ReferralBillingMappingPoco() { }

        public ReferralBillingMappingPoco(ReferralBillingMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            ReferralId = domain.ReferralId;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
