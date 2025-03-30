using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PayorPaymentsPoco
    {
        public Guid PayorPaymentID { get; set; }
        public Guid? PayorID { get; set; }
        public string CheckNo { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Comments { get; set; }
        public Guid? PayorContactID { get; set; }
        public bool? Reconciled { get; set; }
        public bool? Hidden { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Historical { get; set; }

        public PayorPaymentsDomain MapToDomainModel()
        {
            PayorPaymentsDomain domain = new PayorPaymentsDomain
            {
                PayorPaymentID = PayorPaymentID,
                PayorID = PayorID,
                CheckNo = CheckNo,
                Amount = Amount,
                PaymentDate = PaymentDate,
                Comments = Comments,
                PayorContactID = PayorContactID,
                Reconciled = Reconciled,
                Hidden = Hidden,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Historical = Historical
            };

            return domain;
        }

        public PayorPaymentsPoco() { }

        public PayorPaymentsPoco(PayorPaymentsDomain domain)
        {
            PayorPaymentID = domain.PayorPaymentID;
            PayorID = domain.PayorID;
            CheckNo = domain.CheckNo;
            Amount = domain.Amount;
            PaymentDate = domain.PaymentDate;
            Comments = domain.Comments;
            PayorContactID = domain.PayorContactID;
            Reconciled = domain.Reconciled;
            Hidden = domain.Hidden;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Historical = domain.Historical;
        }
    }
}
