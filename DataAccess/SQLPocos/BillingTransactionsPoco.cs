using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingTransactionsPoco
    {
        public Guid BillingTransactionID { get; set; }
        public Guid BillingID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal? TransactionAmount { get; set; }
        public int? PaymentMethodID { get; set; }
        public string CheckNo { get; set; }
        public string TransactionComment { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? PaymentTypeID { get; set; }

        public BillingTransactionsDomain MapToDomainModel()
        {
            BillingTransactionsDomain domain = new BillingTransactionsDomain
            {
                BillingTransactionID = BillingTransactionID,
                BillingID = BillingID,
                TransactionDate = TransactionDate,
                TransactionAmount = TransactionAmount,
                PaymentMethodID = PaymentMethodID,
                CheckNo = CheckNo,
                TransactionComment = TransactionComment,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                PaymentTypeID = PaymentTypeID
            };

            return domain;
        }

        public BillingTransactionsPoco() { }

        public BillingTransactionsPoco(BillingTransactionsDomain domain)
        {
            BillingTransactionID = domain.BillingTransactionID;
            BillingID = domain.BillingID;
            TransactionDate = domain.TransactionDate;
            TransactionAmount = domain.TransactionAmount;
            PaymentMethodID = domain.PaymentMethodID;
            CheckNo = domain.CheckNo;
            TransactionComment = domain.TransactionComment;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            PaymentTypeID = domain.PaymentTypeID;
        }
    }
}

