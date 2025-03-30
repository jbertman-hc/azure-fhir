using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SuperbillPaymentsPoco
    {
        public int SuperbillPaymentID { get; set; }
        public Guid BillingID { get; set; }
        public int PatientID { get; set; }
        public decimal? Copay { get; set; }
        public decimal? Other { get; set; }
        public decimal? Adjustments { get; set; }
        public string CopayComment { get; set; }
        public string OtherComment { get; set; }
        public string AdjustmentComment { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string BalanceComment { get; set; }

        public SuperbillPaymentsDomain MapToDomainModel()
        {
            SuperbillPaymentsDomain domain = new SuperbillPaymentsDomain
            {
                SuperbillPaymentID = SuperbillPaymentID,
                BillingID = BillingID,
                PatientID = PatientID,
                Copay = Copay,
                Other = Other,
                Adjustments = Adjustments,
                CopayComment = CopayComment,
                OtherComment = OtherComment,
                AdjustmentComment = AdjustmentComment,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                BalanceComment = BalanceComment
            };

            return domain;
        }

        public SuperbillPaymentsPoco() { }

        public SuperbillPaymentsPoco(SuperbillPaymentsDomain domain)
        {
            SuperbillPaymentID = domain.SuperbillPaymentID;
            BillingID = domain.BillingID;
            PatientID = domain.PatientID;
            Copay = domain.Copay;
            Other = domain.Other;
            Adjustments = domain.Adjustments;
            CopayComment = domain.CopayComment;
            OtherComment = domain.OtherComment;
            AdjustmentComment = domain.AdjustmentComment;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            BalanceComment = domain.BalanceComment;
        }
    }
}
