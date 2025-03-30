using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitProviderAdjustmentsPoco
    {
        public Guid ID { get; set; }
        public Guid RemitProviderAdjustementsHeaderID { get; set; }
        public string AdjustmentReasonCode { get; set; }
        public string AdjustmentRefID { get; set; }
        public decimal AdjustmentAMT { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RemitProviderAdjustmentsDomain MapToDomainModel()
        {
            RemitProviderAdjustmentsDomain domain = new RemitProviderAdjustmentsDomain
            {
                ID = ID,
                RemitProviderAdjustementsHeaderID = RemitProviderAdjustementsHeaderID,
                AdjustmentReasonCode = AdjustmentReasonCode,
                AdjustmentRefID = AdjustmentRefID,
                AdjustmentAMT = AdjustmentAMT,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RemitProviderAdjustmentsPoco() { }

        public RemitProviderAdjustmentsPoco(RemitProviderAdjustmentsDomain domain)
        {
            ID = domain.ID;
            RemitProviderAdjustementsHeaderID = domain.RemitProviderAdjustementsHeaderID;
            AdjustmentReasonCode = domain.AdjustmentReasonCode;
            AdjustmentRefID = domain.AdjustmentRefID;
            AdjustmentAMT = domain.AdjustmentAMT;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
