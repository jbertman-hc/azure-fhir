using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SuperbillMappingPoco
    {
        public int UniqueTableId { get; set; }
        public Guid BillingGuid { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? PartnerTransactionId { get; set; }
        public DateTime? DateLastSent { get; set; }
        public string LastResultMessage { get; set; }
        public bool? IsFailure { get; set; }

        public SuperbillMappingDomain MapToDomainModel()
        {
            SuperbillMappingDomain domain = new SuperbillMappingDomain
            {
                UniqueTableId = UniqueTableId,
                BillingGuid = BillingGuid,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                PartnerTransactionId = PartnerTransactionId,
                DateLastSent = DateLastSent,
                LastResultMessage = LastResultMessage,
                IsFailure = IsFailure
            };

            return domain;
        }

        public SuperbillMappingPoco() { }

        public SuperbillMappingPoco(SuperbillMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            BillingGuid = domain.BillingGuid;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            PartnerTransactionId = domain.PartnerTransactionId;
            DateLastSent = domain.DateLastSent;
            LastResultMessage = domain.LastResultMessage;
            IsFailure = domain.IsFailure;
        }
    }
}
