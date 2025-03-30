using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitAdjustments_BackupPoco
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
        public DateTime? OriginalKey { get; set; }

        public RemitAdjustments_BackupDomain MapToDomainModel()
        {
            RemitAdjustments_BackupDomain domain = new RemitAdjustments_BackupDomain
            {
                AdjustmentID = AdjustmentID,
                RemitClaimsID = RemitClaimsID,
                RemitServiceLinesID = RemitServiceLinesID,
                AdjustmentAMT = AdjustmentAMT,
                AdjustmentCode = AdjustmentCode,
                AdjustmentReason = AdjustmentReason,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                OriginalKey = OriginalKey
            };

            return domain;
        }

        public RemitAdjustments_BackupPoco() { }

        public RemitAdjustments_BackupPoco(RemitAdjustments_BackupDomain domain)
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
            OriginalKey = domain.OriginalKey;
        }
    }
}
