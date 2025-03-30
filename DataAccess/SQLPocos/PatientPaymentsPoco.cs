using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PatientPaymentsPoco
    {
        public Guid PatientPaymentsID { get; set; }
        public int? PatientID { get; set; }
        public decimal? Amount { get; set; }
        public string Comments { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string CheckNo { get; set; }
        public int? CreditCardType { get; set; }
        public bool? AcctCorrection { get; set; }
        public bool? Assigned { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Historical { get; set; }

        public PatientPaymentsDomain MapToDomainModel()
        {
            PatientPaymentsDomain domain = new PatientPaymentsDomain
            {
                PatientPaymentsID = PatientPaymentsID,
                PatientID = PatientID,
                Amount = Amount,
                Comments = Comments,
                PaymentDate = PaymentDate,
                CheckNo = CheckNo,
                CreditCardType = CreditCardType,
                AcctCorrection = AcctCorrection,
                Assigned = Assigned,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Historical = Historical
            };

            return domain;
        }

        public PatientPaymentsPoco() { }

        public PatientPaymentsPoco(PatientPaymentsDomain domain)
        {
            PatientPaymentsID = domain.PatientPaymentsID;
            PatientID = domain.PatientID;
            Amount = domain.Amount;
            Comments = domain.Comments;
            PaymentDate = domain.PaymentDate;
            CheckNo = domain.CheckNo;
            CreditCardType = domain.CreditCardType;
            AcctCorrection = domain.AcctCorrection;
            Assigned = domain.Assigned;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Historical = domain.Historical;
        }
    }
}
