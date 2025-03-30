using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SuperbillAccountPoco
    {
        public int ID { get; set; }
        public int BillingID { get; set; }
        public int PatientID { get; set; }
        public decimal? TotalCharges { get; set; }
        public decimal? Copay { get; set; }
        public decimal? Insur1 { get; set; }
        public decimal? Insur2 { get; set; }
        public decimal? Other { get; set; }
        public decimal? Adjustments { get; set; }
        public decimal? Balance { get; set; }
        public string BillingComments { get; set; }
        public string SavedBy { get; set; }
        public DateTime SavedDate { get; set; }
        public string commentCopay { get; set; }
        public string commentIns1 { get; set; }
        public string commentIns2 { get; set; }
        public string commentOther { get; set; }
        public string commentAdjust { get; set; }
        public string commentBalance { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SuperbillAccountDomain MapToDomainModel()
        {
            SuperbillAccountDomain domain = new SuperbillAccountDomain
            {
                ID = ID,
                BillingID = BillingID,
                PatientID = PatientID,
                TotalCharges = TotalCharges,
                Copay = Copay,
                Insur1 = Insur1,
                Insur2 = Insur2,
                Other = Other,
                Adjustments = Adjustments,
                Balance = Balance,
                BillingComments = BillingComments,
                SavedBy = SavedBy,
                SavedDate = SavedDate,
                commentCopay = commentCopay,
                commentIns1 = commentIns1,
                commentIns2 = commentIns2,
                commentOther = commentOther,
                commentAdjust = commentAdjust,
                commentBalance = commentBalance,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SuperbillAccountPoco() { }

        public SuperbillAccountPoco(SuperbillAccountDomain domain)
        {
            ID = domain.ID;
            BillingID = domain.BillingID;
            PatientID = domain.PatientID;
            TotalCharges = domain.TotalCharges;
            Copay = domain.Copay;
            Insur1 = domain.Insur1;
            Insur2 = domain.Insur2;
            Other = domain.Other;
            Adjustments = domain.Adjustments;
            Balance = domain.Balance;
            BillingComments = domain.BillingComments;
            SavedBy = domain.SavedBy;
            SavedDate = domain.SavedDate;
            commentCopay = domain.commentCopay;
            commentIns1 = domain.commentIns1;
            commentIns2 = domain.commentIns2;
            commentOther = domain.commentOther;
            commentAdjust = domain.commentAdjust;
            commentBalance = domain.commentBalance;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
