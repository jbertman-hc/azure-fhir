using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PatientCharges_BackupPoco
    {
        public Guid PatientChargesID { get; set; }
        public int? PatientID { get; set; }
        public int? ChargeTypeID { get; set; }
        public decimal? Amount { get; set; }
        public bool? FullyPaid { get; set; }
        public DateTime? DateEntered { get; set; }
        public string Comments { get; set; }
        public Guid? RemitServiceLinesID { get; set; }
        public Guid? BillingCPTsID { get; set; }
        public Guid? BillingID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Ledger { get; set; }
        public bool? ReconciledToRemit { get; set; }
        public bool? Historical { get; set; }
        public string ChargeText { get; set; }
        public string ChargeName { get; set; }
        public DateTime? OriginalKey { get; set; }

        public PatientCharges_BackupDomain MapToDomainModel()
        {
            PatientCharges_BackupDomain domain = new PatientCharges_BackupDomain
            {
                PatientChargesID = PatientChargesID,
                PatientID = PatientID,
                ChargeTypeID = ChargeTypeID,
                Amount = Amount,
                FullyPaid = FullyPaid,
                DateEntered = DateEntered,
                Comments = Comments,
                RemitServiceLinesID = RemitServiceLinesID,
                BillingCPTsID = BillingCPTsID,
                BillingID = BillingID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Ledger = Ledger,
                ReconciledToRemit = ReconciledToRemit,
                Historical = Historical,
                ChargeText = ChargeText,
                ChargeName = ChargeName,
                OriginalKey = OriginalKey
            };

            return domain;
        }

        public PatientCharges_BackupPoco() { }

        public PatientCharges_BackupPoco(PatientCharges_BackupDomain domain)
        {
            PatientChargesID = domain.PatientChargesID;
            PatientID = domain.PatientID;
            ChargeTypeID = domain.ChargeTypeID;
            Amount = domain.Amount;
            FullyPaid = domain.FullyPaid;
            DateEntered = domain.DateEntered;
            Comments = domain.Comments;
            RemitServiceLinesID = domain.RemitServiceLinesID;
            BillingCPTsID = domain.BillingCPTsID;
            BillingID = domain.BillingID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Ledger = domain.Ledger;
            ReconciledToRemit = domain.ReconciledToRemit;
            Historical = domain.Historical;
            ChargeText = domain.ChargeText;
            ChargeName = domain.ChargeName;
            OriginalKey = domain.OriginalKey;
        }
    }
}
