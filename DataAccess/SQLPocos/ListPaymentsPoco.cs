using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPaymentsPoco
    {
        public Guid ListPaymentsID { get; set; }
        public Guid? PatientPaymentsID { get; set; }
        public Guid? PatientChargesID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPaymentsDomain MapToDomainModel()
        {
            ListPaymentsDomain domain = new ListPaymentsDomain
            {
                ListPaymentsID = ListPaymentsID,
                PatientPaymentsID = PatientPaymentsID,
                PatientChargesID = PatientChargesID,
                Amount = Amount,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPaymentsPoco() { }

        public ListPaymentsPoco(ListPaymentsDomain domain)
        {
            ListPaymentsID = domain.ListPaymentsID;
            PatientPaymentsID = domain.PatientPaymentsID;
            PatientChargesID = domain.PatientChargesID;
            Amount = domain.Amount;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
