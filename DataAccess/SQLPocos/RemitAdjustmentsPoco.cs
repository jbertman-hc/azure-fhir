using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitAdjustmentsPoco
    {
        public Guid AdjustmentID { get; set; }
        public Guid? RemitClaimsID { get; set; }
        public Guid? RemitServiceLinesID { get; set; }
        public decimal AdjustmentAMT { get; set; }
        public string AdjustmentCode { get; set; }
        public string AdjustmentReason { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RemitAdjustmentsDomain MapToDomainModel()
        {
            RemitAdjustmentsDomain domain = new RemitAdjustmentsDomain
            {
                AdjustmentID = AdjustmentID,
                RemitClaimsID = RemitClaimsID,
                RemitServiceLinesID = RemitServiceLinesID,
                AdjustmentAMT = AdjustmentAMT,
                AdjustmentCode = AdjustmentCode,
                AdjustmentReason = AdjustmentReason,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RemitAdjustmentsPoco() { }

        public RemitAdjustmentsPoco(RemitAdjustmentsDomain domain)
        {
            AdjustmentID = domain.AdjustmentID;
            RemitClaimsID = domain.RemitClaimsID;
            RemitServiceLinesID = domain.RemitServiceLinesID;
            AdjustmentAMT = domain.AdjustmentAMT;
            AdjustmentCode = domain.AdjustmentCode;
            AdjustmentReason = domain.AdjustmentReason;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
